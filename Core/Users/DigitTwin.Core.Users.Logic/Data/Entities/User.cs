using DigitTwin.Lib.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

namespace DigitTwin.Core.Users
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
        /// ФИО
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public byte[]? Password { get; set; }

        /// <summary>
        /// Хранение ключа шифрования
        /// </summary>
        public byte[]? PasswordSalt { get; set; }

        /// <summary>
        /// Тип пользователя <see cref="UserTypeEnum"/>
        /// </summary>
        public UserTypeEnum Type { get; set; }

        /// <summary>
        /// Статус активности пользователя <see cref="UserStatusEnum"/>
        /// </summary>
        public UserStatusEnum Status { get; set; }

        /// <summary>
        /// ИД организации
        /// </summary>
        public virtual Guid? OrganizationId { get; set; }

        /// <summary>
        /// Организация
        /// </summary>
        public virtual Organization? Organization { get; set; }
    }
}
