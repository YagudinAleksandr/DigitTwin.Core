using DigitTwin.Lib.Abstractions;

namespace DigitTwin.Core.Services.Users
{
    /// <inheritdoc cref="IUserRepository{TKey, TEntity}"/>
    public class UserRepository : IUserRepository<Guid, User>
    {
        Task<User?> IBaseService<Guid, User>.Create(User entity)
        {
            throw new NotImplementedException();
        }

        Task IBaseService<Guid, User>.Delete(User entity)
        {
            throw new NotImplementedException();
        }

        Task<User?> IBaseService<Guid, User>.GetById(Guid key)
        {
            throw new NotImplementedException();
        }

        Task<User?> IBaseService<Guid, User>.Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
