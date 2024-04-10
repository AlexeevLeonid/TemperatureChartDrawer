using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Requests;

namespace TempAnAr.Persistence.Interfaces
{
    public interface IUserRepository
    {
        public Task<IUser?> LoginAsync(LoginDetails details);
        public Task<IUser> GetUserAsync(Guid id);
        public Task<bool> IsUsernameNotTakenAsync(string username);
        public Task PostUserAsync(IUser user);

    }
}
