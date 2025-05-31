using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitTwin.Infrastructure.DataContext
{
    /// <summary>
    /// Подключение контейнера DI для базы данных
    /// </summary>
    public static class DiExtensions
    {
        /// <summary>
        /// Подключение DI контекста баз данных
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        /// <param name="configSection">Секция в конфигурационном файле</param>
        /// <param name="migrationsAssembly">Сборка для миграций</param>
        /// <param name="configurationsAssembly">Конфигурация сборки для БД</param>
        /// <returns>Сервисы</returns>
        /// <exception cref="ArgumentException">Ощибка аргумента</exception>
        public static IServiceCollection AddDatabaseContext(
            this IServiceCollection services,
            IConfiguration configuration,
            Assembly configurationsAssembly,
            string configSection = "Database",
            string? migrationsAssembly = null)
        {
            // Получаем настройки БД
            var dbOptions = new ContextConfiguration();
            configuration.GetSection(configSection).Bind(dbOptions);

            if (string.IsNullOrEmpty(dbOptions.Type))
            {
                throw new ArgumentException("Database type is not configured");
            }

            if (string.IsNullOrEmpty(dbOptions.ConnectionString))
            {
                throw new ArgumentException("Connection string is not configured");
            }

            // Определяем сборку для миграций
            migrationsAssembly ??= Assembly.GetCallingAssembly().GetName().Name;

            // Регистрируем контекст в зависимости от типа БД
            switch (dbOptions.Type.ToUpper())
            {
                case "NPG":
                    services.AddDbContext<DigitTwingMainContext, PostgreDbContext>(options =>
                        options.UseNpgsql(
                            dbOptions.ConnectionString,
                            x => x.MigrationsAssembly(migrationsAssembly)));
                    break;

                case "MSQ":
                    services.AddDbContext<DigitTwingMainContext, MsSqlDbContext>(options =>
                        options.UseSqlServer(
                            dbOptions.ConnectionString,
                            x => x.MigrationsAssembly(migrationsAssembly)));
                    break;

                default:
                    throw new ArgumentException($"Invalid database type: {dbOptions.Type}. Use 'NPG' or 'MSQ'.");
            }

            // Регистрируем DatabaseOptions как синглтон
            services.AddSingleton(dbOptions);

            var configurationTypes = configurationsAssembly.GetTypes()
                .Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(EntityConfiguration)));

            foreach (var configType in configurationTypes)
            {
                services.AddSingleton(typeof(EntityConfiguration), configType);
            }

            return services;
        }
    }
}
