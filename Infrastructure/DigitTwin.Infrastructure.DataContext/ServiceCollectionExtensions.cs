using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitTwin.Infrastructure.DataContext
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// DI сборки
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        /// <param name="configurationsAssembly">Сборка конфигураций</param>
        /// <param name="configSection">Секция настроек</param>
        /// <returns>DI</returns>
        /// <exception cref="ArgumentException">Неверные настройки</exception>
        public static IServiceCollection AddCoreEf(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly configurationsAssembly,
        string configSection = "Database")
        {
            // Загрузка настроек БД
            var dbOptions = new DatabaseOptions();
            configuration.GetSection(configSection).Bind(dbOptions);

            // Валидация настроек
            if (string.IsNullOrEmpty(dbOptions.DbType))
                throw new ArgumentException("Database type is not configured");
            if (string.IsNullOrEmpty(dbOptions.ConnectionString))
                throw new ArgumentException("Connection string is not configured");

            // Регистрация конфигураций
            RegisterEntityConfigurations(services, configurationsAssembly);

            // Регистрация контекста
            services.AddDbContext<AppDbContext>(options =>
            {
                switch (dbOptions.DbType.ToUpper())
                {
                    case "NPG":
                        options.UseNpgsql(dbOptions.ConnectionString);
                        break;

                    case "MSQ":
                        options.UseSqlServer(dbOptions.ConnectionString);
                        break;

                    default:
                        throw new ArgumentException($"Invalid database type: {dbOptions.DbType}. Use 'NPG' or 'MSQ'.");
                }
            });

            // Регистрация конкретных контекстов
            services.AddScoped<PostgreDbContext>(provider =>
                (PostgreDbContext)provider.GetRequiredService<AppDbContext>());

            services.AddScoped<MsSqlDbContext>(provider =>
                (MsSqlDbContext)provider.GetRequiredService<AppDbContext>());

            return services;
        }

        private static void RegisterEntityConfigurations(
            IServiceCollection services,
            Assembly configurationsAssembly)
        {
            // Находим все неабстрактные реализации IEntityTypeConfiguration
            var configurationTypes = configurationsAssembly.GetTypes()
                .Where(t => !t.IsAbstract &&
                            typeof(IEntityTypeConfiguration).IsAssignableFrom(t));

            foreach (var configType in configurationTypes)
            {
                // Регистрируем как IEntityTypeConfiguration
                services.AddSingleton(typeof(IEntityTypeConfiguration), configType);
            }
        }
    }
}
