using System.Linq.Expressions;

namespace DigitTwin.Lib.Abstractions
{
    /// <summary>
    /// Базовый фильтр
    /// </summary>
    public interface IBaseFilter<T>
    {
        /// <summary>
        /// Критерии выборки
        /// </summary>
        Expression<Func<T, bool>> Criteria { get; }

        /// <summary>
        /// Включает в себя
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; }

        /// <summary>
        /// Включает в себя
        /// </summary>
        List<string> IncludeStrings { get; }
    }
}
