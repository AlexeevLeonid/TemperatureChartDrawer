using System.ComponentModel.DataAnnotations.Schema;
using TempArAn.Domain.AbstractCore;

namespace TempArAn.Domain.Models.User
{
    public class User : IUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string? Token { get; set; }
        [NotMapped]
        public string[] Roles { get; set; }

        public string? AuthenticationType => "JwtBearer";

        public bool IsAuthenticated => true;

        public User(string name, string password)
        {
            if (name.Length == 0 && name.Length > 20) throw new ArgumentException(nameof(name));
            Name = name;
            Password = password;
            Id = Guid.NewGuid();
            Roles = Array.Empty<string>();
        }
    }
}
