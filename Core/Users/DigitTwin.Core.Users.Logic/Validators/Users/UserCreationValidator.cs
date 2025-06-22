using DigitTwin.Lib.Contracts;
using DigitTwin.Lib.Translations;
using FluentValidation;
using System.Text.RegularExpressions;

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

            RuleFor(x => x.Password)
                .MinimumLength(8).WithMessage(ValidationMessage.MinLengthExceeded(nameof(UserCreateDto.Password), 8))
                .MaximumLength(30).WithMessage(ValidationMessage.MaxLengthExceeded(nameof(UserCreateDto.Password), 30))
                .Must(password => Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&]).{8,}$"))
                .WithMessage("Пароль должен содержать минимум 8 символов, включая строчную букву, заглавную букву, цифру и специальный символ (@$!%*#?&)");
        }
    }
}
