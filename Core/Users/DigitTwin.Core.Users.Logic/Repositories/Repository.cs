namespace DigitTwin.Core.Users.Logic.Repositories
{
    internal class Repository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class
    {
        public Task<TEntity?> Create(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity?> GetById(TKey key)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity?> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
