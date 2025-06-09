using DigitTwin.Lib.Abstractions;

namespace DigitTwin.Core.Users.Logic.Filters.Users
{
    /// <summary>
    /// Получение одной записи пользователя по ИД
    /// </summary>
    internal class GetSingleUserById : BaseFilter<User>
    {
        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="id">ИД пользователя</param>
        public GetSingleUserById(Guid id) : base(i => i.Id == id)
        {
        }
    }
}
