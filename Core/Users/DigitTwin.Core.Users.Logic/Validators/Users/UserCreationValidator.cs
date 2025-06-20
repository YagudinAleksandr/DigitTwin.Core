using DigitTwin.Lib.Contracts;
using DigitTwin.Lib.Translations;
using FluentValidation;

namespace DigitTwin.Core.Users.Logic.Validators.Users
{
    /// <summary>
    /// Валидатор создания пользователя
    /// </summary>
    internal class UserCreationValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreationValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ValidationMessage.RequiredField(nameof(UserCreateDto.Email)))
                .EmailAddress().WithMessage(ValidationMessage.InvalidEmail(nameof(UserCreateDto.Email)))
                .MinimumLength(4).WithMessage(ValidationMessage.MinLengthExceeded(nameof(UserCreateDto.Email), 4))
                .MaximumLength(60).WithMessage(ValidationMessage.MaxLengthExceeded(nameof(UserCreateDto.Email), 60));
        }
    }
}
