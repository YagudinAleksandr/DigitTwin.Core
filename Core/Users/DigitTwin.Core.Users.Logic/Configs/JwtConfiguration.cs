namespace DigitTwin.Core.Users.Logic.Configs
{
    /// <summary>
    /// Конфигурация JWT токенов
    /// </summary>
    public class JwtConfiguration
    {
        /// <summary>
        /// Секция настроек
        /// </summary>
        public const string SectionName = "JwtSettings";

        /// <summary>
        /// Секретный ключ для подписи JWT
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary>
        /// Издатель токена
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Аудитория токена
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Время жизни токена
        /// </summary>
        public TimeSpan ExpirationToken { get; set; } = TimeSpan.FromDays(1); // 24 часа по умолчанию

        /// <summary>
        /// Время жизни токена обновления
        /// </summary>
        public TimeSpan ExpirationRefresh { get; set; } = TimeSpan.FromDays(7); // 7 суток

        /// <summary>
        /// Алгоритм подписи
        /// </summary>
        public string Algorithm { get; set; } = "HS256";
    }
} 