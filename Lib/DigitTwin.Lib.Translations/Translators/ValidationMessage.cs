using System.Globalization;
using System.Reflection;
using System.Resources;

namespace DigitTwin.Lib.Translations
{
    /// <summary>
    /// Выбор языковых ресурсов (полей) для вывода
    /// </summary>
    public class ValidationMessage
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("DigitTwin.Lib.Translations.Resources.ValidationMessage",
                Assembly.GetExecutingAssembly());

        public static string RequiredField(string fieldName) =>
            Format("RequiredField", fieldName);

        public static string InvalidEmail(string fieldName) =>
            Format("InvalidEmail", fieldName);

        public static string MaxLengthExceeded(string fieldName, int maxLength) =>
            Format("MaxLengthExceeded", fieldName, maxLength);

        public static string MinLengthExceeded(string fieldName, int minLength) =>
            Format("MinLengthExceeded", fieldName, minLength);

        private static string Format(string key, params object[] args)
        {
            // Получаем строку из ресурсов или используем fallback
            var value = _resourceManager.GetString(key, CultureInfo.CurrentCulture);

            // Обработка случая, когда ресурс не найден
            if (string.IsNullOrEmpty(value))
            {
                value = $"[[{key}]]"; // Заменитель для отсутствующего ресурса
            }

            // Форматируем строку, если есть аргументы
            return args.Length > 0
                ? string.Format(value, args)
                : value;
        }
    }
}
