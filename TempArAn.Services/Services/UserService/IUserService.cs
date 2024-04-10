using Microsoft.AspNetCore.Http;
using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Requests;

namespace TempArAn.Services.Services.UserService
{
    public interface IUserService
    {
        Task<IUser> AuthenticateAsync(LoginDetails details);

        Task<IUser> RegisterAsync(LoginDetails details);
        Task<IUser> GetUserFromContextAsync(HttpContext context);
        Task<IUser> GetUserByIdAsync(Guid id);
        Guid GetUserIdFromContextAsync(HttpContext context);
        Task<bool> IsUsernameNotTakenAsync(string username);
    }
}
