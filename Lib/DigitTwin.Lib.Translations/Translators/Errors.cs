using System.Globalization;
using System.Reflection;
using System.Resources;

namespace DigitTwin.Lib.Translations
{
    /// <summary>
    /// Выбор языковых ресурсов (полей) для ошибок
    /// </summary>
    public static class Errors
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("DigitTwin.Lib.Translations.Resources.Errors",
                Assembly.GetExecutingAssembly());

        public static string AlreadyExist(string name) => Format("AlreadyExist", name);

        public static string CannotCreate(string name) => Format("CannotCreate", name);

        public static string CannotFind(string name, string paramName, string val) => Format("CannotFind", name, paramName, val);

        public static string CannotUpdate(string name) => Format("CannotUpdate", name);

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
