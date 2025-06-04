namespace DigitTwin.Infrastructure.DatabaseContext
{
    /// <summary>
    /// Настройки базовых параметров БД
    /// </summary>
    public static class DefaultDatabaseParams
    {
        /// <summary>
        /// Наименьший диапозон символов
        /// </summary>
        public const int SMALL_STRING_LENGTH = 30;

        /// <summary>
        /// Маленьки диапозон символов
        /// </summary>
        public const int SHORT_STRING_LENGTH = 60;

        /// <summary>
        /// Стандартный диапозон символов
        /// </summary>
        public const int NORMAL_STRING_LENGTH = 255;

        /// <summary>
        /// Текст
        /// </summary>
        public const int LARGE_TEXT_LENGTH = 512;
    }
}
