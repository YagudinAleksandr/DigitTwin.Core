using Serilog;
using Serilog.Context;
using Serilog.Events;

namespace DigitTwin.Infrastructure.LoggerSeq
{
    /// <inheritdoc cref="ILogger"/>
    internal class LoggerService : ILoggerService
    {
        private readonly ILogger _logger;

        public LoggerService(string url, LogEventLevel level, string applicationName)
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Is(level)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", applicationName)
                .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown")
                .WriteTo.Seq(serverUrl: url)
                .CreateLogger();
        }
        public void LogDebug(string service, string message)
        {
            using (LogContext.PushProperty("ServiceName", service))
            {
                _logger.Debug(message);
            }
            
        }

        public void LogError(string service, string message, Exception? ex = null)
        {
            using (LogContext.PushProperty("ServiceName", service))
            {
                _logger.Error(message, ex);
            }
        }

        public void LogInformation(string service, string message)
        {
            using (LogContext.PushProperty("ServiceName", service))
            {
                _logger.Information(message);
            }
        }

        public void LogWarning(string service, string message)
        {
            using (LogContext.PushProperty("ServiceName", service))
            {
                _logger.Warning(message);
            }
        }
    }
}
