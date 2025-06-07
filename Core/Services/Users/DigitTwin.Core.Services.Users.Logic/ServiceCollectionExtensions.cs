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
        public static IServiceCollection AddUserService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddActionService(configuration);
            services.AddDB(configuration);
            services.AddAutoMapper(typeof(UserDtoMapper).Assembly);
            services.AddRepositories();
            services.AddServices();
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
                .AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        }

        /// <summary>
        /// Добавление сервисов
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <returns>DI контейнер</returns>
        private static IServiceCollection AddServices(this IServiceCollection services) 
        {
            return services
                .AddSingleton(typeof(IUserService), typeof(UserService));
        }
    }
}
