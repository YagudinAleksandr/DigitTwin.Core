using DigitTwin.Infrastructure.DatabaseContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitTwin.Core.Services.Users.Logic
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
            var settings = configuration.GetSection("DatabaseSettings");
            var dbType = Enum.Parse<DatabaseType>(settings["Type"]!);
            var connectionString = settings["ConnectionString"]!;

            services.AddDatabaseContext<UsersDbContext>(dbType, connectionString);

            return services;
        }
    }
}
