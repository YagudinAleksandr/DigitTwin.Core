using Microsoft.EntityFrameworkCore;

namespace DigitTwin.Core.Services.Users
{
    internal class UserRepository : IUserRepository<Guid, User>
    {
        #region CTOR
        /// <inheritdoc cref="UsersDbContext"/>
        private readonly UsersDbContext _context;

        /// <inheritdoc cref="DbSet{TEntity}"/>
        private readonly DbSet<User> _users;

        public UserRepository(UsersDbContext context)
        {
            _context = context;
            _users = _context.Set<User>();
        }
        #endregion
        public async Task<User?> Create(User entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(User entity)
        {
            _users.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<User>> GetAllByFilter(GetSingleUserFilter<User> filter)
        {
            IQueryable<User> query = _users.AsQueryable();

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

        public async Task<User?> GetById(Guid key)
        {
            return await _users.FindAsync(key);
        }

        public async Task<User?> GetSingleByFilter(GetSingleUserFilter<User> filter)
        {
            IQueryable<User> query = _users.AsQueryable();

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

        public async Task<User?> Update(User entity)
        {
            _users.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
