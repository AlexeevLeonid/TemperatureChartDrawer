using TempArAn.Application.Base;
using TempArAn.Domain.Requests;
using TempArAn.Domain.Responses;

namespace TempArAn.Application.User.Requests
{
    public class LoginCommand : RequestLoginDetails<UserResponse>
    {
        public LoginCommand(LoginDetails loginDetails) : base(loginDetails)
        {
        }
    }
}
