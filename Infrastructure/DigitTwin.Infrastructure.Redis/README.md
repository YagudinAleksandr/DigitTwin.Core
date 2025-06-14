# DigitTwin.Infrastructure.Redis
Универсальная библиотека для работы с Redis в .NET приложениях

## Особенности
- Поддержка любых моделей данных через generics
- Настройка через конфигурационный файл
- Автоматическая сериализация/десериализация JSON
- Гибкое управление TTL (время жизни записей)

## Настройка подключения

`appsettings.json`:
```json
"RedisSettings": {
  "ConnectionString": "ваш_сервер:порт",
  "DefaultTTLSeconds": 300
}
```

`Регистрация в DI`:
```chsarp
builder.Services.AddRedisService(builder.Configuration);
```

## Использование
```csharp
public class WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
}

public class WeatherService
{
    private readonly IRedisService<WeatherForecast> _redis;

    public WeatherService(IRedisService<WeatherForecast> redis)
    {
        _redis = redis;
    }

    public async Task CacheWeatherAsync(string key, WeatherForecast forecast)
    {
        // Сохранение с TTL по умолчанию
        await _redis.SetAsync(key, forecast);
        
        // Сохранение с кастомным TTL (20 секунд)
        await _redis.SetAsync(key, forecast, TimeSpan.FromSeconds(20));
    }

    public async Task<WeatherForecast> GetCachedWeatherAsync(string key)
    {
        return await _redis.GetAsync(key);
    }
}
```

## Методы API
`SetAsync(key, value, ttl?)`: Сохранение объекта. `ttl` - опциональное время жизни (TimeSpan)

`GetAsync(key)`: Получение объекта

`RemoveAsync(key)`: Удаление записи

`ExistsAsync(key)`: Проверка существования ключа

## Конфигурация
| Параметр          | Описание                      | По умолчанию |
|-------------------|-------------------------------|--------------|
| ConnectionString  | Строка подключения к Redis    | Обязательный |
| DefaultTTLSeconds | Время жизни записей (секунды) | 300          |