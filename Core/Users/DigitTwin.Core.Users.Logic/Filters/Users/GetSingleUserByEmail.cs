using DigitTwin.Lib.Abstractions;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Получение пользователя по E-mail
    /// </summary>
    internal class GetSingleUserByEmail : BaseFilter<User>
    {
        public GetSingleUserByEmail(string email) : base(e=>e.Email == email)
        {
            
        }
    }
}
