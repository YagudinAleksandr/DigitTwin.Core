using DigitTwin.Lib.Abstractions;

namespace DigitTwin.Core.Users.Logic.Data.Entities
{
    /// <summary>
    /// Связь пользователей с организацией
    /// </summary>
    public class OrganizationUser : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// ИД пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// ИД организации
        /// </summary>
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// Тип пользователя
        /// </summary>
        public OrganizationUsersTypeEnum UsersType { get; set; }
    }
}
