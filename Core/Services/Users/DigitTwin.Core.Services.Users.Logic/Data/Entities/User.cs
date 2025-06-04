using DigitTwin.Lib.Abstractions;

namespace DigitTwin.Core.Services.Users
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; } = null!;

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
        public string Name { get; set; } = null!;

        /// <summary>
        /// Тип <see cref="UserTypeEnum"/>
        /// </summary>
        public UserTypeEnum Type { get; set; }

        /// <summary>
        /// Статус <see cref="UserStatusEnum"/>
        /// </summary>
        public UserStatusEnum Status { get; set; }
    }
}
