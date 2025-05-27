using System.ComponentModel.DataAnnotations;

namespace DigitTwin.Lib.Abstractions
{
    /// <summary>
    /// Типы пользователей
    /// </summary>
    public enum UserTypeEnum
    {
        [Display(Name = "Система")]
        SYSTEM = 0,

        [Display(Name = "Пользователь")]
        USER = 1,

        [Display(Name = "Администратор")]
        ADMINISTRATOR = 2
    }
}
