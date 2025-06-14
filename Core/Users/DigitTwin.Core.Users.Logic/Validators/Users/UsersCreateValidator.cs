using DigitTwin.Lib.Contracts.User;
using DigitTwin.Lib.Validator;

namespace DigitTwin.Core.Users
{
    internal static class UsersCreateValidator
    {
        /// <summary>
        /// Валидатор на создание пользователя
        /// </summary>
        public static ValidationResult Run(this UserCreateDto createUser)
        {
            var validator = new Validator<UserCreateDto>();

            validator.RuleFor(u => u.Email)
                .Must(e => string.IsNullOrEmpty(e), "E-mail не может быть пустым полем")
                .Must(e => e.Contains('@'), "E-mail не валиден")
                .Must(e => e.Length <= 60, "E-mail не может быть больше 60 символов");

            validator.RuleFor(u => u.Name)
                .Must(e => e.Length <= 60, "ФИО не может быть больше 60 символов");

            return validator.Validate(createUser);
        }
    }
}
