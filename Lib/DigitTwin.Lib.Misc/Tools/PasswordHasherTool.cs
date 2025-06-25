using System.Security.Cryptography;
using System.Text;

namespace DigitTwin.Lib.Misc.Tools
{
    /// <summary>
    /// Утилита для работы с хэшами паролей
    /// </summary>
    public static class PasswordHasherTool
    {
        /// <summary>
        /// Формирование хэша пароля
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <param name="passwordHash">Хэш</param>
        /// <param name="passwordSalt">Ключ</param>
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();

            passwordSalt = hmac.Key;

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            passwordHash = hmac.ComputeHash(passwordBytes); 
        }

        /// <summary>
        /// Верификация пароля
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <param name="storedHash">Хэш</param>
        /// <param name="storedSalt">Ключ</param>
        /// <returns>true - верно, false - ошибка</returns>
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            byte[] computedHash = hmac.ComputeHash(passwordBytes);

            bool hashesMatch = computedHash.SequenceEqual(storedHash);

            return hashesMatch;
        }
    }
}
