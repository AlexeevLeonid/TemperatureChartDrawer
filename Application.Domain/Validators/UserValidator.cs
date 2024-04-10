using FluentValidation;
using TempArAn.Domain.AbstractCore;

namespace TempArAn.Domain.Validators
{
    public class UserValidator : AbstractValidator<IUser>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(20).MinimumLength(4);
            RuleFor(x => x.Password).NotEmpty().MaximumLength(20).MinimumLength(4);
        }
    }
}
