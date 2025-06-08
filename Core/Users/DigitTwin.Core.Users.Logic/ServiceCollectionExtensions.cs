using DigitTwin.Infrastructure.DataContext;
using DigitTwin.Infrastructure.LoggerSeq;
using DigitTwin.Lib.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitTwin.Core.Users.Logic
{
    /// <summary>
    /// Расширения DI для работы с сервисом для пользователей
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Формирование DI для сервиса работы с пользователями
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>DI контейнер</returns>
        public static IServiceCollection AddUserServiceLogic(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddLogger(configuration);
            services.AddDatabaseContext(configuration);

            return services;
        }

        /// <summary>
        /// Подключение всех зависимостей для работы с базой данных
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>DI контейнер</returns>
        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection(DatabaseSettings.SectionName));

            services.AddSingleton(typeof(IDbContextFactory<>), typeof(DbContextFactory<>));
            services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();

            return services;
        }
    }
}
