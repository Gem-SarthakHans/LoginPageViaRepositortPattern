using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace LoginPageViaRepositortPattern.Views.Clients
{
    public class IndexModel : PageModel
    {
        public List<clientinfo> listclients= new List<clientinfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=10.50.18.16;Initial Catalog=training_db;Persist Security Info=True;User ID=SVC_TRANING;Password=***********";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM tblUsers";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clientinfo clientinfo = new clientinfo();
                                clientinfo.fullname = reader.GetString(0);
                                clientinfo.email = reader.GetString(1);
                                clientinfo.password = reader.GetString(2);

                                listclients.Add(clientinfo);

                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine("Exception: " +ex.ToString());
            }
        }
    }

    public class clientinfo {

        public String fullname;
        public String email;
        public String password;
    
    
    }

}
