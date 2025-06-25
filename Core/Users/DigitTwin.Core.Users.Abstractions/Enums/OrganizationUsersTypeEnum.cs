using System.ComponentModel.DataAnnotations;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Типы пользователей организации
    /// </summary>
    public enum OrganizationUsersTypeEnum
    {
        [Display(Name = "Пользователь")]
        User = 0,

        [Display(Name = "Менеджер")]
        Manager = 1,

        [Display(Name = "Администратор")]
        Administrator = 2
    }
}
