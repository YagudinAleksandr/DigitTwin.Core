using DigitTwin.Lib.Abstractions;

namespace DigitTwin.Core.Services.Users
{
    /// <summary>
    /// Сущность пользователя
    /// </summary>
    internal class User : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username { get; set; } = null!;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// ФИО
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get; set; } = null!;

        /// <summary>
        /// Статус активности пользователя <see cref="UserStatusEnum"/>
        /// </summary>
        public UserStatusEnum Status { get; set; } = UserStatusEnum.Inactive;

        /// <summary>
        /// Тип пользователя <see cref="UserTypeEnum"/>
        /// </summary>
        public UserTypeEnum Type { get; set; } = UserTypeEnum.User;
    }
}
