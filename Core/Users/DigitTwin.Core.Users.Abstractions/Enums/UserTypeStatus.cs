using System.ComponentModel.DataAnnotations;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Статусы пользователя
    /// </summary>
    public enum UserTypeStatus
    {
        [Display(Name = "Не активен")]
        NOT_ACTIVE = 0,

        [Display(Name = "Активен")]
        ACTIVE = 1,

        [Display(Name = "Заблокирован")]
        BLOCKED = 2
    }
}
