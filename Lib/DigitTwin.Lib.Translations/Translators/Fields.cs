using System.Globalization;
using System.Reflection;
using System.Resources;

namespace DigitTwin.Lib.Translations
{
    /// <summary>
    /// Выбор языковых ресурсов (полей) для полей
    /// </summary>
    public static class Fields
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("DigitTwin.Lib.Translations.Resources.Fields",
                Assembly.GetExecutingAssembly());

        public static string BankAccount() => Format("BankAccount");
        public static string CorAccount() => Format("CorAccount");
        public static string Email() => Format("Email");
        public static string Inn() => Format("Inn");
        public static string Kpp() => Format("Kpp");
        public static string Name() => Format("Name");
        public static string Ogrn() => Format("Ogrn");
        public static string OrganizationFullName() => Format("OrganizationFullName");
        public static string OrganizationName() => Format("OrganizationName");

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
