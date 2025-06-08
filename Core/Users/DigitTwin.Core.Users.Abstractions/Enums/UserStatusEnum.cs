using System.ComponentModel.DataAnnotations;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Статус активности пользователя
    /// </summary>
    public enum UserStatusEnum
    {
        [Display(Name = "Не активен")]
        NotActive = 0,

        [Display(Name = "Активен")]
        Active = 1,
    }
}
