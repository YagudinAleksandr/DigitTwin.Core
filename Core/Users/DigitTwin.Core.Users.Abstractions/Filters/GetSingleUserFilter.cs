using DigitTwin.Lib.Abstractions.Filters;

namespace DigitTwin.Core.Users.Abstractions.Filters
{
    public class GetSingleUserFilter : IBaseFilter
    {
        public Guid Id { get; set; }
    }
}
