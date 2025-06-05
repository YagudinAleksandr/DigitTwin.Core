# WebSocket Server Library
Библиотека для управления WebSocket-соединениями в .NET Standard 2.1

## Особенности
- Поддержка нескольких одновременных соединений
- Отправка сообщений отдельным клиентам и широковещательная рассылка
- Интеграция с ASP.NET Core и Unity
- Унифицированная система сообщений

## Использование в ASP.NET Core
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddWebSocketManager();
}
```

### Добавление middleware:
```csharp
public void Configure(IApplicationBuilder app)
{
    app.UseWebSocketServer();
    // Другие middleware...
}
```

### Отправка сообщений из контроллера:
```csharp
public class MessageController : ControllerBase
{
    private readonly IWebSocketServer _webSocketServer;

    public MessageController(IWebSocketServer webSocketServer)
    {
        _webSocketServer = webSocketServer;
    }

    [HttpPost("send/{connectionId}")]
    public async Task<IActionResult> SendMessage(string connectionId, [FromBody] string message)
    {
        await _webSocketServer.SendMessageAsync(connectionId, new WebSocketMessage
        {
            MessageType = "text",
            Data = message
        });
        return Ok();
    }
}
```

## Использование в Unity
Импортируйте DLL в Assets/Plugins
Создайте фасад для инициализации:

```csharp
public class WebSocketInitializer : MonoBehaviour
{
    void Start()
    {
        var services = new ServiceCollection();
        services.AddWebSocketManager();
        ServiceProvider = services.BuildServiceProvider();
    }

    public IWebSocketServer GetWebSocketServer()
    {
        return ServiceProvider.GetService<IWebSocketServer>();
    }
}
```
### Пример отправки сообщения:
```csharp
public class GameController : MonoBehaviour
{
    private IWebSocketServer _webSocketServer;

    void Start()
    {
        _webSocketServer = FindObjectOfType<WebSocketInitializer>()
            .GetWebSocketServer();
    }

    public void SendScoreUpdate(string playerId, int score)
    {
        _webSocketServer.SendMessageAsync(playerId, new WebSocketMessage
        {
            MessageType = "scoreUpdate",
            Data = score.ToString()
        });
    }
}
```

### Сообщения
```json
{
  "MessageType": "your_message_type",
  "Data": "json_or_string_data"
}
```