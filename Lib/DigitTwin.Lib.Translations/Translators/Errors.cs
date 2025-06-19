using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace DigitTwin.Lib.Translations.Translators
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
