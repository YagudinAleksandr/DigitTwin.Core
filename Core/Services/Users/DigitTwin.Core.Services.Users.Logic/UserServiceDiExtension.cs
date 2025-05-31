using DigitTwin.Infrastructure.DataContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitTwin.Core.Services.Users
{
    public static class UserServiceDiExtension
    {
        public static IServiceCollection AddUserService(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddRepositories(configuration);
            return services;
        }

        /// <summary>
        /// Подключение репозиториев
        /// </summary>
        /// <param name="services">DI сервисов</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        internal static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseContext(configuration, typeof(UserConfiguration).Assembly);
            services.AddSingleton(typeof(IUserRepository<,>), typeof(UserRepository));

            return services;
        }
    }
}
