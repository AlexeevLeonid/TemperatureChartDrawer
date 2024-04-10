using TempArAn.Application.Base;
using TempArAn.Domain.Requests;
using TempArAn.Domain.Responses;

namespace TempArAn.Application.User.Requests
{
    public class RegisterCommand : RequestLoginDetails<UserResponse>
    {
        public RegisterCommand(LoginDetails loginDetails) : base(loginDetails)
        {
        }
    }
}
