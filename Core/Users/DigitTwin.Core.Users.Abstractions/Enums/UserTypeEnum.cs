using System.ComponentModel.DataAnnotations;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Типы пользователей
    /// </summary>
    public enum UserTypeEnum
    {
        [Display(Name = "Система")]
        System = 0,

        [Display(Name = "Пользователь")]
        User = 1,

        [Display(Name = "Администратор")]
        Administrator = 2
    }
}
