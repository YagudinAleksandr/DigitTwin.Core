using System.Linq.Expressions;
using DigitTwin.Lib.Abstractions;

namespace DigitTwin.Core.Services.Users
{
    public class GetSingleUserFilter<TEntity> : BaseFilter<TEntity>
    {
        public GetSingleUserFilter(Expression<Func<TEntity, bool>> criteria) : base(criteria)
        {
        }
    }
}
