using DigitTwin.Infrastructure.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitTwin.Core.ActionService
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление сервиса <inheritdoc cref="IActionService"/>
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <returns>DI контейнер</returns>
        public static IServiceCollection AddActionService(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddRedisService(configuration);
            services.AddJwtConfiguration(configuration);

            services.AddSingleton<ITokenService, TokenService>();

            services.AddScoped<IActionService, ActionService>();
            services.AddScoped<TokenAuthFilter>();
            services.AddScoped<TokenRefreshFilter>();
            services.AddScoped<TokenRoleFilter>();

            return services;
        }

        /// <summary>
        /// Добавление конфигурации JWT
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>DI контейнер</returns>
        private static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfiguration>(configuration.GetSection(JwtConfiguration.SectionName));

            return services;
        }
    }
}
