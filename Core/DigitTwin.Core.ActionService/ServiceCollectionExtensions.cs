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
        public static IServiceCollection AddActionService(this IServiceCollection services) 
        {
            services.AddSingleton<IActionService, ActionService>();

            return services;
        }
    }
}
