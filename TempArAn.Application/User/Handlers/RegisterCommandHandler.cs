using AutoMapper;
using MediatR;
using TempArAn.Application.User.Requests;
using TempArAn.Domain.Responses;
using TempArAn.Services.Services.UserService;

namespace TempArAn.Application.User.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public RegisterCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<UserResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.RegisterAsync(request.LoginDetails);
            return _mapper.Map<UserResponse>(user);
        }
    }
}
