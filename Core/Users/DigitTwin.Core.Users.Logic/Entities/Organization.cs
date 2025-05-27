using DigitTwin.Lib.Abstractions;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Организация
    /// </summary>
    public class Organization : IEntity<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Краткое наименование организации
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Полное название организации
        /// </summary>
        public string LawName { get; set; } = null!;

        /// <summary>
        /// ИНН
        /// </summary>
        public string Inn { get; set; } = null!;

        /// <summary>
        /// КПП
        /// </summary>
        public string Kpp { get; set; } = null!;

        /// <summary>
        /// ОГРН
        /// </summary>
        public string Ogrn { get; set; } = null!;

        /// <summary>
        /// Расчетный счет
        /// </summary>
        public string Account { get; set; } = null!;

        /// <summary>
        /// Корреспондентский счет
        /// </summary>
        public string CorrespondentAccount { get; set; } = null!;

        /// <summary>
        /// Фактический адрес
        /// </summary>
        public string PostalAddress { get; set; } = null!;

        /// <summary>
        /// Юридический адрес
        /// </summary>
        public string BuisnessAddress { get; set; } = null!;
    }
}
