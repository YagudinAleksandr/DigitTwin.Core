using DigitTwin.Lib.Contracts.User;
using DigitTwin.Lib.Validator;

namespace DigitTwin.Core.Services.Users
{
    /// <summary>
    /// Валидатор запроса на создание пользователя
    /// </summary>
    public static class UserCreateRequestValidator
    {
        public static ValidationResult Run(this UserCreateDto creationUser)
        {
            var validator = new Validator<UserCreateDto>();

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
