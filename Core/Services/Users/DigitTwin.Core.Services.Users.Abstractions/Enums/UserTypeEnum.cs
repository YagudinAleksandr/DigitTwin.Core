using System.ComponentModel.DataAnnotations;

namespace DigitTwin.Core.Services.Users
{
    /// <summary>
    /// Типы пользователя
    /// </summary>
    public enum UserTypeEnum
    {
        /// <summary>
        /// Система
        /// </summary>
        [Display(Name = "Система")]
        System = 0,

        /// <summary>
        /// Пользователь
        /// </summary>
        [Display(Name = "Пользователь")]
        User = 1,

        /// <summary>
        /// Администратор
        /// </summary>
        [Display(Name = "Администратор")]
        Administrator = 2
    }
}
