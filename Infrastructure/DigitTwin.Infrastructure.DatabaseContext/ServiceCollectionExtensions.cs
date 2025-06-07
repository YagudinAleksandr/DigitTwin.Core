using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        /// <returns>DI контейнер</returns>
        public static IServiceCollection AddDatabaseContext<TContext>(
            this IServiceCollection services,
            IConfigurationSection configurationSection)
            where TContext : ApplicationDbContext
        {
            var configuration = CreateDatabaseConfig(configurationSection);

            services.AddDbContext<TContext>(options =>
            {
                ConfigureOptions(options, configuration.Type, configuration.ConnectionString);
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

        /// <summary>
        /// Конфигурация БД
        /// </summary>
        /// <param name="configurationSection">Секция</param>
        /// <returns><see cref="DatabaseConfig"/></returns>
        private static DatabaseConfig CreateDatabaseConfig(IConfigurationSection configurationSection)
        {
            var dbType = Enum.Parse<DatabaseType>(configurationSection["Type"]!);
            var connectionString = configurationSection["ConnectionString"]!;

            return new DatabaseConfig() { Type = dbType, ConnectionString = connectionString };
        }
    }
}
