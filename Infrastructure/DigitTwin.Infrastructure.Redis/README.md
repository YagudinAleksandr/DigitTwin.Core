# DigitTwin.Infrastructure.Redis
������������� ���������� ��� ������ � Redis � .NET �����������

## �����������
- ��������� ����� ������� ������ ����� generics
- ��������� ����� ���������������� ����
- �������������� ������������/�������������� JSON
- ������ ���������� TTL (����� ����� �������)

## ��������� �����������

`appsettings.json`:
```json
"RedisSettings": {
  "ConnectionString": "���_������:����",
  "DefaultTTLSeconds": 300
}
```

`����������� � DI`:
```chsarp
builder.Services.AddRedisService(builder.Configuration);
```

## �������������
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
        // ���������� � TTL �� ���������
        await _redis.SetAsync(key, forecast);
        
        // ���������� � ��������� TTL (20 ������)
        await _redis.SetAsync(key, forecast, TimeSpan.FromSeconds(20));
    }

    public async Task<WeatherForecast> GetCachedWeatherAsync(string key)
    {
        return await _redis.GetAsync(key);
    }
}
```

## ������ API
`SetAsync(key, value, ttl?)`: ���������� �������. `ttl` - ������������ ����� ����� (TimeSpan)

`GetAsync(key)`: ��������� �������

`RemoveAsync(key)`: �������� ������

`ExistsAsync(key)`: �������� ������������� �����

## ������������
| ��������          | ��������                      | �� ��������� |
|-------------------|-------------------------------|--------------|
| ConnectionString  | ������ ����������� � Redis    | ������������ |
| DefaultTTLSeconds | ����� ����� ������� (�������) | 300          |