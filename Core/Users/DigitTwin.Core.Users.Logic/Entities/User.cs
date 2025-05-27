using DigitTwin.Lib.Abstractions;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Тип пользователя
        /// </summary>
        public UserTypeEnum Type { get; set; } = UserTypeEnum.USER;

        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// Статус активности
        /// </summary>
        public UserTypeStatus Status { get; set; } = UserTypeStatus.NOT_ACTIVE;

        /// <summary>
        /// ИД организации
        /// </summary>
        public Guid? OrganizationId { get; set; }

        /// <summary>
        /// Организация <see cref="Organization"/>
        /// </summary>
        public virtual Organization? Organization { get; set; }
    }
}
