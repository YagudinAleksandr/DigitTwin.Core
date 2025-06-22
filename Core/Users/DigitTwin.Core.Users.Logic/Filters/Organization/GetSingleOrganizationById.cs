using DigitTwin.Lib.Abstractions;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Получение организации по ИД
    /// </summary>
    internal class GetSingleOrganizationById : BaseFilter<Organization>
    {
        public GetSingleOrganizationById(Guid id) : base(x => x.Id == id)
        {
        }
    }
}
