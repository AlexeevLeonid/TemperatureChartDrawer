using AutoMapper;
using MediatR;
using TempArAn.Application.User.Requests;
using TempArAn.Domain.Responses;
using TempArAn.Services.Services.UserService;

namespace TempArAn.Application.User.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, UserResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public LoginCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<UserResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.AuthenticateAsync(request.LoginDetails);
            var responce = _mapper.Map<UserResponse>(result);
            return responce;
        }
    }
}
