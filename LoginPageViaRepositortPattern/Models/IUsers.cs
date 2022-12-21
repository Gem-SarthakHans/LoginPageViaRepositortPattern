namespace LoginPageViaRepositortPattern.Models
{
    public interface IUsers
    {
        bool Verify(string email, string password);

        bool Register(Users u);

        bool FindDuplicate(string email);
    }
}
