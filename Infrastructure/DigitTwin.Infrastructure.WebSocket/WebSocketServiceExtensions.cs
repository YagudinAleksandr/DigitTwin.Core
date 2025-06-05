using DigitTwin.Infrastructure.WS;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DigitTwin.Infrastructure.WS
{
    public static class WebSocketServiceExtensions
    {
        /// <summary>
        /// Подключение WebSocket
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <returns>DI контейнер</returns>
        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddSingleton<IWebSocketConnectionManager, WebSocketConnectionManager>();
            services.AddScoped<IWebSocketServer, WebSocketServer>();
            return services;
        }

        /// <summary>
        /// Формирование сервера
        /// </summary>
        /// <param name="builder">Формирование конвеера приложжения</param>
        /// <returns>Конвеер</returns>
        public static IApplicationBuilder UseWebSocketServer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebSocketMiddleware>();
        }
    }
}
