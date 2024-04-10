using MediatR;
using TempArAn.Application.User.Requests;
using TempArAn.Services.Services.UserService;

namespace TempArAn.Application.User.Handlers
{
    public class IsNotTakenQueryHandler : IRequestHandler<IsNotTakenQuery, bool>
    {
        private readonly IUserService _userService;

        public IsNotTakenQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(IsNotTakenQuery request, CancellationToken cancellationToken)
        {
            return await _userService.IsUsernameNotTakenAsync(request.Name);
        }
    }
}
