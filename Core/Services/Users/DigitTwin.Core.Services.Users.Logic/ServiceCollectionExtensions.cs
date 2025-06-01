using DigitTwin.Infrastructure.DatabaseContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitTwin.Core.Services.Users
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление сервисов работы с пользователями
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>DI контейнер</returns>
        public static IServiceCollection AddUserServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDB(configuration);
            return services;
        }

        /// <summary>
        /// Подключение контекса БД
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>DI контейнер</returns>
        private static IServiceCollection AddDB(this IServiceCollection services, IConfiguration configuration) 
        {
            var settings = configuration.GetSection("DatabaseSettings");
            services.AddDatabaseContext<UsersDbContext>(settings);

            return services;
        }
    }
}
