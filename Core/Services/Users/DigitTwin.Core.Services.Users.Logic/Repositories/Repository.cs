using DigitTwin.Lib.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DigitTwin.Core.Services.Users
{
    
    internal class Repository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class
    {
        #region CTOR
        /// <inheritdoc cref="UsersDbContext"/>
        private readonly UsersDbContext _context;

        /// <inheritdoc cref="DbSet{TEntity}"/>
        private readonly DbSet<TEntity> _table;

        public Repository(UsersDbContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }
        #endregion
        public async Task<TEntity?> Create(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(TEntity entity)
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllByFilter(IBaseFilter<TEntity> filter)
        {
            IQueryable<TEntity> query = _table.AsQueryable();

            // Применяем условия WHERE
            if (filter.Criteria != null)
            {
                query = query.Where(filter.Criteria);
            }

            // Добавляем INCLUDE через выражения
            query = filter.Includes
                .Aggregate(query, (current, include) => current.Include(include));

            // Добавляем INCLUDE через строки (для вложенных свойств)
            query = filter.IncludeStrings
                .Aggregate(query, (current, include) => current.Include(include));

            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetById(TKey key)
        {
            return await _table.FindAsync(key);
        }

        public async Task<TEntity?> GetSingleByFilter(IBaseFilter<TEntity> filter)
        {
            IQueryable<TEntity> query = _table.AsQueryable();

            // Применяем условия WHERE
            if (filter.Criteria != null)
            {
                query = query.Where(filter.Criteria);
            }

            // Добавляем INCLUDE через выражения
            query = filter.Includes
                .Aggregate(query, (current, include) => current.Include(include));

            // Добавляем INCLUDE через строки (для вложенных свойств)
            query = filter.IncludeStrings
                .Aggregate(query, (current, include) => current.Include(include));

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TEntity?> Update(TEntity entity)
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
