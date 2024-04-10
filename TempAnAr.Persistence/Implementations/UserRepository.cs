using TempAnAr.Persistence.Base;
using TempAnAr.Persistence.Context;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Domain.Models.User;

namespace TempAnAr.Persistence.Implementations
{
    public class UserRepository : UserRepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context, context.Users) { }
    }
}
