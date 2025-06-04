using DigitTwin.Lib.EventBroker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitTwin.Lib.Misc
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление библиотечных сервисов
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>DI контейнер</returns>
        public static IServiceCollection AddMiscServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddSingleton<IEventBroker, EventBroker.EventBroker>();

            return services;
        }
    }
}
