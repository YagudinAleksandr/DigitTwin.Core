using DigitTwin.Core.ActionService;
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
            services.AddActionService(configuration);
            services.AddDB(configuration);
            services.AddRepositories();
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

        /// <summary>
        /// Добавление репозиториев
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <returns>DI контейнер</returns>
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddSingleton(typeof(IUserRepository<,>), typeof(UserRepository));
        }
    }
}
