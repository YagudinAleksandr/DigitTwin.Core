# Сервис логирования для работы через SEQ

## Подключение через DI
```csharp
services.AddLogger(services, configuration);
```
Конфигурация
```json
{
  "LoggerSeq": {
    "Url": "http://localhost:5341",
    "MinLevel": "Information"
  }
}
```

## Использование
```csharp

private readonly ILoggerService _logger;

_logger.LogInformation("Worker started at: {Time}", DateTime.UtcNow);
_logger.LogDebug("Debug sample");
_logger.LogWarning("Warning sample");
_logger.LogError("Critical error occurred", ex);
```