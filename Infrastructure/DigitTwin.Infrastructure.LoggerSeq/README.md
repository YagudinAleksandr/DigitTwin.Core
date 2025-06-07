# ������ ����������� ��� ������ ����� SEQ

## ����������� ����� DI
```csharp
services.AddLogger(services, configuration);
```
������������
```json
{
  "LoggerSeq": {
    "Url": "http://localhost:5341",
    "MinLevel": "Information",
    "ApplicationName": "Application.Test"
  }
}
```

## �������������
```csharp

private readonly ILoggerService _logger;

_logger.LogInformation("ServiceName", "Worker started at: {Time}", DateTime.UtcNow);
_logger.LogDebug("ServiceName", "Debug sample");
_logger.LogWarning("ServiceName", "Warning sample");
_logger.LogError("ServiceName", "Critical error occurred", ex);
```