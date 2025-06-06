using DigitTwin.Lib.Contracts.User;
using DigitTwin.Lib.Validator;

namespace DigitTwin.Core.Services.Users
{
    /// <summary>
    /// Валидатор обновления пользователя
    /// </summary>
    public static class UserUpdateRequestValidator
    {
        public static ValidationResult Run(this UserDto creationUser)
        {
            var validator = new Validator<UserDto>();

            validator.RuleFor(u => u.Id)
                .Must(l => !string.IsNullOrEmpty(l.ToString()), "ID не может быть Null");

            validator.RuleFor(u => u.Email)
                .Must(email => !string.IsNullOrEmpty(email), "E-mail должен быть указан")
                .Must(email => email.Length >= 5, "E-mail должен состоять минимум из 5 символов")
                .Must(email => email.Length <= 60, "E-mail не может быть больше 60 символов")
                .Must(email => email.Contains('@'), "Не валидный E-mail");

            validator.RuleFor(u => u.Name)
                .Must(name => name.Length <= 30, "ФИО не может быть больше 30 символов");

            return validator.Validate(creationUser);
        }
    }
}
