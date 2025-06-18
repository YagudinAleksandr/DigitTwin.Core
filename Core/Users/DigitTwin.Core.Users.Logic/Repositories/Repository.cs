using DigitTwin.Core.Users.Logic.Data;
using DigitTwin.Infrastructure.DataContext;
using DigitTwin.Infrastructure.LoggerSeq;
using DigitTwin.Lib.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DigitTwin.Core.Users
{
    /// <inheritdoc cref="IRepository{TKey, TEntity}"/>
    internal class Repository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class, IEntity<TKey>
    {
        #region CTOR
        /// <summary>
        /// Название сервиса
        /// </summary>
        private const string ServiceName = nameof(Repository<TKey, TEntity>);

        /// <inheritdoc cref="IDbContextFactory{TContext}"/>
        private readonly Infrastructure.DataContext.IDbContextFactory<UserDbContext> _context;

        /// <inheritdoc cref="ILoggerService"/>
        private readonly ILoggerService _logger;

        public Repository(Infrastructure.DataContext.IDbContextFactory<UserDbContext> context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion

        public async Task<TEntity?> Create(TEntity entity)
        {
            try
            {
                using var context = _context.CreateDbContext();

                await context.AddAsync(entity);
                await context.SaveChangesAsync();

                return entity;
            }
            catch(Exception ex)
            {
                _logger.LogError(ServiceName, "Can not create entity", ex);
                return null;
            }
        }

        public async Task Delete(TEntity entity)
        {
            try
            {
                using var context = _context.CreateDbContext();
                var entities = context.Set<TEntity>();

                entities.Remove(entity);
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ServiceName, "Can not delete entity", ex);
            }
        }

        public async Task<TEntity?> GetByFilter(IBaseFilter<TEntity> filter)
        {
            try
            {
                using var context = _context.CreateDbContext();
                var entities = context.Set<TEntity>();
                var entity = await entities.FirstOrDefaultAsync(filter.Criteria);

                if (entity == null)
                {
                    _logger.LogWarning(ServiceName, $"Cannot find entity with params {filter.Criteria}");
                    return null;
                }

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ServiceName, "Cannot filter entity", ex);
                return null;
            }
        }

        public async Task<IReadOnlyCollection<TEntity>> GetByFilterCollection(IBaseFilter<TEntity> filter)
        {
            try
            {
                using var context = _context.CreateDbContext();
                var entities = context.Set<TEntity>();
                return await entities.Where(filter.Criteria).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ServiceName, "Cannot filter entities", ex);
                return [];
            }
        }

        public async Task<TEntity?> Update(TEntity entity)
        {
            try
            {
                using var context = _context.CreateDbContext();
                var entities = context.Set<TEntity>();

                entities.Update(entity);

                await context.SaveChangesAsync();

                return entity;
            }
            catch(Exception ex)
            {
                _logger.LogError(ServiceName, "Can not update entity", ex);
                return entity;
            }
        }
    }
}
