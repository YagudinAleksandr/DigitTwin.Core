using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace DigitTwin.Infrastructure.Redis
{
    /// <summary>
    /// Расширение для подключения сборки с Redis
    /// </summary>
    public static class RedisServiceExtensions
    {
        /// <summary>
        /// Сервис для работы с Redis
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>DI контейнер</returns>
        public static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RedisConfiguration>(_ => configuration.GetSection(RedisConfiguration.SectionName));

            services.AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect(
                    configuration["RedisSettings:ConnectionString"]!));

            services.AddSingleton(typeof(IRedisService<>), typeof(RedisService<>));

            return services;
        }
    }
}
