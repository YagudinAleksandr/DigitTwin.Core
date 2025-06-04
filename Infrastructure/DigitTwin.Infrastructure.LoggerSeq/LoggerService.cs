using Serilog;
using Serilog.Events;

namespace DigitTwin.Infrastructure.LoggerSeq
{
    /// <inheritdoc cref="ILogger"/>
    internal class LoggerService : ILoggerService
    {
        private readonly ILogger _logger;

        public LoggerService(string url, LogEventLevel level)
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Is(level)
                .Enrich.FromLogContext()
                .WriteTo.Seq(serverUrl: url)
                .CreateLogger();
        }
        public void LogDebug(string message) => _logger.Debug(message);

        public void LogError(string message, Exception? ex = null) => _logger.Error(message, ex);

        public void LogInformation(string message) => _logger.Information(message);

        public void LogWarning(string message)=>_logger.Warning(message);
    }
}
