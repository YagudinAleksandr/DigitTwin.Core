namespace DigitTwin.Lib.Abstractions
{
    /// <summary>
    /// Одиночный фильтр
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// Список фильтров
        /// </summary>
        public Dictionary<string, object> Filters { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Сортировка по полю
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// Сортировка по возрастанию или убыванию
        /// </summary>
        public string? SortOrder { get; set; }

        // Валидация
        public bool IsValid()
        {
            if (!string.IsNullOrEmpty(SortOrder) &&
                !new[] { "asc", "desc" }.Contains(SortOrder.ToLower()))
                return false;

            return true;
        }
    }
}
