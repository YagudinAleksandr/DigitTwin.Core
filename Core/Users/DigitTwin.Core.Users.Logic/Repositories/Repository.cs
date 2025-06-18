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

        /// <inheritdoc cref="DbSet{TEntity}"/>
        private readonly DbSet<TEntity> _entities;

        /// <inheritdoc cref="ILoggerService"/>
        private readonly ILoggerService _logger;

        public Repository(Infrastructure.DataContext.IDbContextFactory<UserDbContext> context, ILoggerService logger)
        {
            _context = context;
            using var cont = _context.CreateDbContext();
            _entities = cont.Set<TEntity>();
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

                _entities.Remove(entity);
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ServiceName, "Can not delete entity", ex);
            }
        }

        public Task<TEntity?> GetByFilter(IBaseFilter<TEntity> filter)
        {
            try
            {
                return Task.Run(() =>
                {
                    var entity = _entities.Where(filter.Criteria).FirstOrDefault();
                    if(entity == null)
                    {
                        _logger.LogWarning(ServiceName, $"Can not finde entity with params {filter.Criteria}");
                        return null;
                    }
                    return entity;
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ServiceName, "Can not filtered entity", ex);
                return null;
            }
        }

        public async Task<IReadOnlyCollection<TEntity>> GetByFilterCollection(IBaseFilter<TEntity> filter)
        {
            try
            {
                return await _entities.Where(filter.Criteria).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ServiceName, "Can not filtered entities", ex);
                return null;
            }
        }

        public async Task<TEntity?> Update(TEntity entity)
        {
            try
            {
                using var context = _context.CreateDbContext();

                _entities.Update(entity);
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
