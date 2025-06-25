using System.Globalization;
using System.Reflection;
using System.Resources;

namespace DigitTwin.Lib.Translations
{
    /// <summary>
    /// Выбор языковых ресурсов (полей) для типов сущностей
    /// </summary>
    public static class Entities
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("DigitTwin.Lib.Translations.Resources.Entities",
                Assembly.GetExecutingAssembly());

        public static string User() => Format("User");
        public static string Organization() => Format("Organization");

        private static string Format(string key)
        {
            // Получаем строку из ресурсов или используем fallback
            var value = _resourceManager.GetString(key, CultureInfo.CurrentCulture);

            // Обработка случая, когда ресурс не найден
            if (string.IsNullOrEmpty(value))
            {
                value = $"[[{key}]]"; // Заменитель для отсутствующего ресурса
            }

            // Форматируем строку, если есть аргументы
            return value;
        }
    }
}
