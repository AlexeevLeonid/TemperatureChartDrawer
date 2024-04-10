using Microsoft.EntityFrameworkCore;
using TempAnAr.Persistence.Context;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Domain.AbstractCore;
using TempArAn.Domain.Requests;

namespace TempAnAr.Persistence.Base
{
    public abstract class UserRepositoryBase<TUser> : IUserRepository
        where TUser : class, IUser
    {
        protected readonly ApplicationContext context;
        protected DbSet<TUser> users;
        public UserRepositoryBase(ApplicationContext context, DbSet<TUser> users)
        {
            this.context = context;
            this.users = users;
        }

        public async Task<IUser> GetUserAsync(Guid id)
        {
            return await users.
                FirstOrDefaultAsync(x => x.Id == id) ??
                throw new ArgumentException("user not found");
        }

        public async Task<bool> IsUsernameNotTakenAsync(string username)
        {
            return await users.
                FirstOrDefaultAsync(p => p.Name == username) == null;
        }

        public async Task<IUser?> LoginAsync(LoginDetails details)
        {
            return await users.
                FirstOrDefaultAsync(p => p.Name == details.Name && p.Password == details.Password);
        }

        public async Task PostUserAsync(IUser user)
        {
            users.
                Add(user as TUser ??
                throw new ArgumentException($"must be {nameof(TUser)} but was {nameof(user)}"));
            await context.SaveChangesAsync();
        }
    }
}