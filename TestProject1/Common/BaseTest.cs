using AutoMapper;
using TempAnAr.Persistence.Context;
using TempAnAr.Persistence.Implementations;
using TempAnAr.Persistence.Interfaces;
using TempArAn.Domain.Validators;
using TempArAn.Services.Services.UserService;

namespace TempArAn.Tests.Common
{
    public abstract class BaseTest
    {
        public ApplicationContext context;
        public IMapper mapper;
        public IUserService userService;
        public IApplicationUnitOfWork unitOfWork;
        public BaseTest(QueryTestFixture fixture)
        {
            context = fixture.context;
            mapper = fixture.mapper;
            unitOfWork = new ApplicationUnitOfWork(context);
            userService = new UserService(unitOfWork, new UserValidator());
        }
    }
}
