using DigitTwin.Lib.Abstractions;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Получение организации по ИНН
    /// </summary>
    internal class GetSingleOrganizationByInn : BaseFilter<Organization>
    {
        public GetSingleOrganizationByInn(string inn) : base(x => x.Inn == inn)
        {
        }
    }
}
