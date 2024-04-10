using TempArAn.Application.User.Handlers;
using TempArAn.Application.User.Requests;
using TempArAn.Domain.Requests;
using TempArAn.Domain.Responses;
using TempArAn.Tests.Common;

namespace TempArAn.Tests.Tests
{
    [Collection("QueryCollection")]
    public class UserRequestsTests : BaseTest
    {
        public UserRequestsTests(QueryTestFixture fixture) : base(fixture) { }

        [Fact]
        public async Task LoginCommandHandler_Success()
        {
            var handler = new LoginCommandHandler(userService, mapper);
            var result = await handler.Handle(
                new LoginCommand(
                    new LoginDetails(
                        ApplicationContextFactory.UserA.Name,
                        ApplicationContextFactory.UserA.Password)),
                    CancellationToken.None);

            Assert.Equal(
                result.Token,
                mapper.Map<UserResponse>(context.Users.FirstOrDefault(x => x.Id == ApplicationContextFactory.UserA.Id)).Token);
        }
        [Fact]
        public async Task RegisterComandHandler_Success()
        {
            var handler = new RegisterCommandHandler(userService, mapper);

            var result = await handler.Handle(
                new RegisterCommand(
                    new LoginDetails(
                        "Test1",
                        "Test1")),
                CancellationToken.None);

            Assert.Equal(
                result.Token,
                mapper.Map<UserResponse>(context.Users.FirstOrDefault(x => x.Name == "Test1")).Token);
        }
        [Fact]
        public async Task IsNotTakenQueryHandler_Success()
        {
            var handler = new IsNotTakenQueryHandler(userService);

            var result = await handler.Handle(
                new IsNotTakenQuery(
                    "a"),
                CancellationToken.None);

            Assert.False(
                result);
        }

    }
}
