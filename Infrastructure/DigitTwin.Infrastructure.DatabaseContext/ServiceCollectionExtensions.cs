using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DigitTwin.Infrastructure.DatabaseContext
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Подключение контекста БД
        /// </summary>
        /// <typeparam name="TContext">Контекст</typeparam>
        /// <param name="services">DI контейнер</param>
        /// <param name="dbType">Тип БД</param>
        /// <param name="connectionString">Строка подключения</param>
        /// <returns>DI контейнер</returns>
        public static IServiceCollection AddDatabaseContext<TContext>(
            this IServiceCollection services,
            DatabaseType dbType,
            string connectionString)
            where TContext : ApplicationDbContext
        {
            services.AddDbContext<TContext>(options =>
            {
                ConfigureOptions(options, dbType, connectionString);
            });

            return services;
        }

        /// <summary>
        /// Конфигурирование контекста
        /// </summary>
        /// <param name="options">Опции</param>
        /// <param name="dbType">Тип БД</param>
        /// <param name="connectionString">Строка подключения</param>
        /// <exception cref="ArgumentException">Ошибка при несуществующем контексте</exception>
        private static void ConfigureOptions(
            DbContextOptionsBuilder options,
            DatabaseType dbType,
            string connectionString)
        {
            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    options.UseSqlServer(connectionString);
                    break;
                case DatabaseType.PostgreSQL:
                    options.UseNpgsql(connectionString);
                    break;
                default:
                    throw new ArgumentException("Unsupported database type");
            }
        }
    }
}
