namespace TempArAn.Domain.Requests
{
    public class LoginDetails
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public LoginDetails(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
