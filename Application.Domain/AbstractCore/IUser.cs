using System.Security.Principal;

namespace TempArAn.Domain.AbstractCore
{
    public interface IUser : IIdentity
    {
        Guid Id { get; set; }
        string Password { get; set; }
        string[] Roles { get; set; }
        string? Token { get; set; }
    }
}