using DigitTwin.Lib.Contracts.User;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DigitTwin.Core.Users.Logic.Validators.Users
{
    internal class UserCreationValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreationValidator(IStringLocalizer<Resources.Resources> localizer)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(localizer["EmailRequired"])
                .EmailAddress().WithMessage(localizer["EmailInvalid"])
                .MinimumLength(4).WithMessage(localizer["EmailMinLength", 4])
                .MaximumLength(60).WithMessage(localizer["EmailMaxLength", 60]);
        }
    }
}
