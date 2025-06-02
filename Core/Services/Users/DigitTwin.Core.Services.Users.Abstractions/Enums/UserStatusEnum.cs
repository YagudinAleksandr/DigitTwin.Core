using System.ComponentModel.DataAnnotations;

namespace DigitTwin.Core.Services.Users
{
    /// <summary>
    /// Статусы пользователя
    /// </summary>
    public enum UserStatusEnum
    {
        [Display(Name = "Не активен")]
        NotActive = 0,

        [Display(Name = "Активен")]
        Active = 1,

        [Display(Name = "Заблокирован")]
        Blocked = 2
    }
}
