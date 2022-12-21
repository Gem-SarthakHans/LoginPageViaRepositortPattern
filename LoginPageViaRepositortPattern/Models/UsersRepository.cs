using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginPageViaRepositortPattern.Models;
using System.Data.SqlClient;


namespace LoginPageViaRepositortPattern.Models
{
    public class UsersRepository : IUsers
    {
        private SqlConnection con = new SqlConnection("Server=10.50.18.16;Database=training_db;User Id=SVC_TRANING;Password=Gemini@123;");
        private SqlCommand com = new SqlCommand();
        private SqlDataReader dr;

        private string str;

        public UsersRepository(string str)
        {
            this.str = str;
        }

        public bool FindDuplicate(string email)
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * From [dbo].[tblUsers] where email='" + email + "'";
            dr = com.ExecuteReader();

            List<Users> u = new List<Users>();

            while (dr.Read())
            {
                var x = new Users
                {
                    fullname = dr.GetString(0),
                    email = dr.GetString(1),
                    password = dr.GetString(3)
                };

                u.Add(x);
            }

            con.Close();

            if (u.Count >= 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public bool Register(Users u)
        {

            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "Insert into [dbo].[tblUsers] values ('" + u.fullname + "','" + u.email + "','" + EncodePasswordToBase64(u.password) + "')";
                com.ExecuteNonQuery();
                con.Close();

                return true;
            }
            catch (Exception)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                return false;
            }
        }
       
        public bool Verify(string email, string password)
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from [dbo].[tblUsers] where email='" + email + "' and password='" + password + "'";
            dr = com.ExecuteReader();

            List<Users> u = new List<Users>();

            while (dr.Read())
            {
                var x = new Users()
                {
                    fullname = dr.GetString(0),
                    email = dr.GetString(1),
                    password = dr.GetString(2),
                    confirmpass = null
                };

                u.Add(x);
            }

            con.Close();

            if (u.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
