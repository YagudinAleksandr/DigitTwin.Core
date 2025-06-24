using System.ComponentModel.DataAnnotations;

namespace DigitTwin.Core.ActionService
{
    /// <summary>
    /// Типы токенов
    /// </summary>
    public enum TokenTypeEnum
    {
        [Display(Name = "Токен авторизации")]
        Auth = 0,

        [Display(Name = "Токен обновления")]
        Refresh = 1
    }
}
