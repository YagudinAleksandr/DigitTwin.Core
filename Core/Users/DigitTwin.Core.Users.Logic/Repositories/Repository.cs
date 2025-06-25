using DigitTwin.Core.Users.Logic.Data;
using DigitTwin.Infrastructure.DataContext;
using DigitTwin.Infrastructure.LoggerSeq;
using DigitTwin.Lib.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

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

        public async Task<(IQueryable<TEntity> entities, int totalCount)> GetAll(Filter filter, int maxPageSize = 10, int startPosition = 1, int endPosition = 10)
        {
            if (startPosition < 1) startPosition = 1;
            if (endPosition < startPosition) return (Enumerable.Empty<TEntity>().AsQueryable(), 0);
            int count = endPosition - startPosition + 1;
            if (count > maxPageSize) count = maxPageSize;

            return await Task.Run(() =>
            {
                var context = _context.CreateDbContext();
                var dbSet = context.Set<TEntity>();
                var query = dbSet.AsQueryable();

                // Применяем фильтры
                if (filter.Filters != null && filter.Filters.Any())
                {
                    var entityType = typeof(TEntity);
                    
                    foreach (var kvp in filter.Filters)
                    {
                        if (kvp.Value != null)
                        {
                            var propertyName = kvp.Key;
                            var propertyValue = kvp.Value;
                            
                            // Проверяем существование свойства
                            var property = entityType.GetProperty(propertyName);
                            if (property != null)
                            {
                                // Применяем фильтр в зависимости от типа свойства
                                if (property.PropertyType == typeof(string))
                                {
                                    // Для строковых полей - поиск по подстроке (содержит)
                                    var stringValue = propertyValue.ToString();
                                    if (!string.IsNullOrEmpty(stringValue))
                                    {
                                        query = query.Where(entity => 
                                            EF.Property<string>(entity, propertyName).Contains(stringValue));
                                    }
                                }
                                else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
                                {
                                    // Для целых чисел - точное совпадение
                                    if (int.TryParse(propertyValue.ToString(), out int intValue))
                                    {
                                        query = query.Where(entity => 
                                            EF.Property<int>(entity, propertyName) == intValue);
                                    }
                                }
                                else if (property.PropertyType == typeof(Guid) || property.PropertyType == typeof(Guid?))
                                {
                                    // Для GUID - точное совпадение
                                    if (Guid.TryParse(propertyValue.ToString(), out Guid guidValue))
                                    {
                                        query = query.Where(entity => 
                                            EF.Property<Guid>(entity, propertyName) == guidValue);
                                    }
                                }
                                else if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
                                {
                                    // Для булевых значений - точное совпадение
                                    if (bool.TryParse(propertyValue.ToString(), out bool boolValue))
                                    {
                                        query = query.Where(entity => 
                                            EF.Property<bool>(entity, propertyName) == boolValue);
                                    }
                                }
                                else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                                {
                                    // Для дат - точное совпадение
                                    if (DateTime.TryParse(propertyValue.ToString(), out DateTime dateValue))
                                    {
                                        query = query.Where(entity => 
                                            EF.Property<DateTime>(entity, propertyName) == dateValue);
                                    }
                                }
                                else if (property.PropertyType == typeof(double) || property.PropertyType == typeof(double?))
                                {
                                    // Для double - точное совпадение
                                    if (double.TryParse(propertyValue.ToString(), out double doubleValue))
                                    {
                                        query = query.Where(entity => 
                                            EF.Property<double>(entity, propertyName) == doubleValue);
                                    }
                                }
                                else if (property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?))
                                {
                                    // Для decimal - точное совпадение
                                    if (decimal.TryParse(propertyValue.ToString(), out decimal decimalValue))
                                    {
                                        query = query.Where(entity => 
                                            EF.Property<decimal>(entity, propertyName) == decimalValue);
                                    }
                                }
                                else if (property.PropertyType.IsEnum)
                                {
                                    // Для enum - точное совпадение
                                    if (Enum.TryParse(property.PropertyType, propertyValue.ToString(), out var enumValue))
                                    {
                                        query = query.Where(entity => 
                                            EF.Property<object>(entity, propertyName).Equals(enumValue));
                                    }
                                }
                                else
                                {
                                    // Для остальных типов - попытка точного совпадения
                                    try
                                    {
                                        var convertedValue = Convert.ChangeType(propertyValue, property.PropertyType);
                                        query = query.Where(entity => 
                                            EF.Property<object>(entity, propertyName).Equals(convertedValue));
                                    }
                                    catch
                                    {
                                        // Игнорируем фильтр если не удалось привести к нужному типу
                                        _logger.LogWarning(ServiceName, $"Cannot apply filter for property {propertyName} with value {propertyValue}");
                                    }
                                }
                            }
                        }
                    }
                }

                // Применяем сортировку
                if (!string.IsNullOrEmpty(filter.SortBy))
                {
                    var sortOrder = filter.SortOrder?.ToLower() == "desc" ? "descending" : "ascending";
                    
                    // Проверяем существование свойства для сортировки
                    var sortProperty = typeof(TEntity).GetProperty(filter.SortBy);
                    if (sortProperty != null)
                    {
                        try
                        {
                            query = query.OrderBy($"{filter.SortBy} {sortOrder}");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ServiceName, $"Cannot apply sorting for property {filter.SortBy}: {ex.Message}");
                        }
                    }
                    else
                    {
                        _logger.LogWarning(ServiceName, $"Sort property {filter.SortBy} not found");
                    }
                }

                var totalCount = query.Count();
                var elements = query
                    .Skip(startPosition - 1)
                    .Take(count);

                return (elements, totalCount);
            });
        }
    }
}
