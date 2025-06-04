using System.Linq.Expressions;

namespace DigitTwin.Lib.Abstractions
{
    /// <summary>
    /// Абстракция фильтра
    /// </summary>
    /// <typeparam name="TEntity">Сущность</typeparam>
    public abstract class BaseFilter<TEntity> : IBaseFilter<TEntity>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; }
        public List<Expression<Func<TEntity, object>>> Includes { get; } = new();
        public List<string> IncludeStrings { get; } = new();

        protected BaseFilter(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }
}
