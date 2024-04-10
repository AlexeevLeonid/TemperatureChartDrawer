using FluentValidation;
using TempArAn.Domain.AbstractCore;

namespace TempArAn.Domain.Validators
{
    public class SourceValidator : AbstractValidator<SourceBase>
    {
        public SourceValidator()
        {
            RuleFor(x => x.Interval).GreaterThan(0);
            RuleFor(x => x.Name).MaximumLength(20).MinimumLength(3);
        }
    }
}
