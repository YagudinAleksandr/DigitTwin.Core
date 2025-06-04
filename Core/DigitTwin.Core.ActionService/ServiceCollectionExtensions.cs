using DigitTwin.Infrastructure.LoggerSeq;
using DigitTwin.Lib.Misc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitTwin.Core.ActionService
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление сервиса действий
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>DI контейнер</returns>
        public static IServiceCollection AddActionService(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddMiscServices(configuration);
            services.AddLogger(configuration);

            services.AddSingleton<IActionResponse, ActionResponse>();

            return services;
        }
    }
}
