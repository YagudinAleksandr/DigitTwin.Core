using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;

namespace DigitTwin.Infrastructure.LoggerSeq
{

    public static class LoggerSeqExtensions
    {
        /// <summary>
        /// Добавление сервиса логирования для работы через SEQ
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>DI контейнер</returns>
        /// <exception cref="ArgumentException">Неверная настройка для подключения к SEQ</exception>
        public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
        {
            var loggerConfiguration = new LoggerConfig()
            {
                Url = configuration["LoggerSeq:Url"],
                MinLevel = configuration["LoggerSeq:MinLevel"],
                ApplicationName = configuration["LoggerSeq:ApplicationName"]
                
            };

            if (string.IsNullOrEmpty(loggerConfiguration.Url)) 
            {
                throw new ArgumentException("LoggerSeq not configured");
            }

            var level = ParseLogLevel(loggerConfiguration.MinLevel!);

            services.AddSingleton<ILoggerService>(_ => new LoggerService(loggerConfiguration.Url, level, loggerConfiguration.ApplicationName));

            return services;
        }

        /// <summary>
        /// Перевод в уровень логирования
        /// </summary>
        /// <param name="level">Уровень логирования в виде строки</param>
        /// <returns><see cref="LogEventLevel"/></returns>
        private static LogEventLevel ParseLogLevel(string level)
        {
            return level switch
            {
                "Verbose" => LogEventLevel.Verbose,
                "Debug" => LogEventLevel.Debug,
                "Information" => LogEventLevel.Information,
                "Warning" => LogEventLevel.Warning,
                "Error" => LogEventLevel.Error,
                "Fatal" => LogEventLevel.Fatal,
                _ => LogEventLevel.Information
            };
        }
    }
}
