# DigitTwin.Infrastructure.Mongo

Сборка для интеграции MongoDB в проекты DigitTwin на .NET. Предоставляет универсальный репозиторий и сервисы для работы с коллекциями MongoDB через DI.

---

## Оглавление
- [Возможности](#возможности)
- [Секция настроек (appsettings.json)](#секция-настроек-appsettingsjson)
- [Подключение через DI](#подключение-через-di)
- [Создание модели](#создание-модели)
- [Использование репозитория в сервисах](#использование-репозитория-в-сервисах)
- [Описание методов IMongoRepository](#описание-методов-imongorepository)
- [Пример контроллера](#пример-контроллера)
- [Структура проекта](#структура-проекта)

---

## Возможности
- Быстрое подключение MongoDB через DI
- Универсальный репозиторий для CRUD и сложных запросов
- Гибкая конфигурация через appsettings.json
- Поддержка проекций, пагинации, агрегаций

---

## Секция настроек (appsettings.json)

```json
"MongoDbConnection": {
  "ConnectionString": "mongodb://localhost:27017",
  "DatabaseName": "DigitTwinDb",
  "ConnectionTimeoutMs": 30000,
  "MaxConnectionPoolSize": 100
}
```

- `ConnectionString` — строка подключения к MongoDB
- `DatabaseName` — имя базы данных
- `ConnectionTimeoutMs` — таймаут соединения (мс)
- `MaxConnectionPoolSize` — максимальный размер пула соединений

---

## Подключение через DI

В `Program.cs` или `Startup.cs`:

```csharp
using DigitTwin.Infrastructure.Mongo;

// ...

builder.Services.AddMongoDb(builder.Configuration);
// Для каждой коллекции:
builder.Services.AddMongoRepository<MyEntity>("MyEntities");
```

- `AddMongoDb` — регистрирует клиент и базу MongoDB
- `AddMongoRepository<T>(collectionName)` — регистрирует репозиторий для коллекции

---

## Создание модели

Модель должна реализовывать интерфейс `IDocument`:

```csharp
using DigitTwin.Infrastructure.Mongo;

public class MyEntity : IDocument
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    // ... другие поля
}
```

---

## Использование репозитория в сервисах

```csharp
public class MyService
{
    private readonly IMongoRepository<MyEntity> _repository;

    public MyService(IMongoRepository<MyEntity> repository)
    {
        _repository = repository;
    }

    public async Task AddAsync(MyEntity entity)
        => await _repository.InsertOneAsync(entity);

    public async Task<MyEntity?> GetByIdAsync(string id)
        => await _repository.FindByIdAsync(id);

    public async Task UpdateAsync(MyEntity entity)
        => await _repository.UpdateOneAsync(entity);

    public async Task DeleteAsync(string id)
        => await _repository.DeleteOneAsync(id);

    public async Task<List<MyEntity>> FindActiveAsync()
        => (await _repository.FindAsync(x => x.IsActive)).ToList();

    public async Task<long> CountActiveAsync()
        => await _repository.CountAsync(x => x.IsActive);

    public async Task<bool> AnyActiveAsync()
        => await _repository.ExistsAsync(x => x.IsActive);

    public async Task<(IEnumerable<MyEntity> items, long total)> GetPagedAsync(int page, int size)
        => await _repository.PaginateAsync(x => true, Builders<MyEntity>.Sort.Descending(e => e.CreatedAt), page, size);

    public async Task<List<string>> GetNamesAsync()
        => (await _repository.ProjectAsync(x => true, x => x.Name)).ToList();
}
```

---

## Описание методов IMongoRepository

- **InsertOneAsync(TDocument document)** — добавить документ
- **FindByIdAsync(string id)** — получить документ по Id
- **UpdateOneAsync(TDocument document)** — обновить документ (по Id)
- **DeleteOneAsync(string id)** — удалить документ по Id
- **FindAsync(filter, sort = null, limit = null)** — найти документы по фильтру, с сортировкой и лимитом
- **ProjectAsync(filter, projection, sort = null)** — получить проекции (выборочные поля)
- **CountAsync(filter)** — количество документов по фильтру
- **ExistsAsync(filter)** — есть ли хотя бы один документ по фильтру
- **Aggregate()** — получить fluent-агрегацию (MongoDB pipeline)
- **PaginateAsync(filter, sort, pageNumber, pageSize)** — постраничный вывод (items, totalCount)

### Примеры вызова методов

```csharp
// Добавить
await _repository.InsertOneAsync(entity);

// Получить по Id
var item = await _repository.FindByIdAsync("id");

// Обновить
await _repository.UpdateOneAsync(entity);

// Удалить
await _repository.DeleteOneAsync("id");

// Найти с фильтром и сортировкой
var items = await _repository.FindAsync(x => x.IsActive, Builders<MyEntity>.Sort.Ascending(x => x.Name), 10);

// Получить только имена
var names = await _repository.ProjectAsync(x => x.IsActive, x => x.Name);

// Количество
long count = await _repository.CountAsync(x => x.IsActive);

// Проверить наличие
bool exists = await _repository.ExistsAsync(x => x.IsActive);

// Пагинация
var (pageItems, total) = await _repository.PaginateAsync(x => true, Builders<MyEntity>.Sort.Descending(x => x.CreatedAt), 1, 20);

// Агрегация (пример)
var aggregate = _repository.Aggregate().Match(x => x.IsActive).Group(x => x.Name, g => new { Name = g.Key, Count = g.Count() }).ToList();
```

---

## Пример контроллера

```csharp
[ApiController]
[Route("api/[controller]")]
public class MyEntitiesController : ControllerBase
{
    private readonly IMongoRepository<MyEntity> _repository;

    public MyEntitiesController(IMongoRepository<MyEntity> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _repository.FindAsync(x => true);
        return Ok(items);
    }
}
```

---

## Структура проекта
- `Configs/` — классы конфигурации
- `Models/` — интерфейсы и базовые модели
- `Services/` — сервисы и репозитории
- `ServiceCollectionExtensions.cs` — extension-методы для DI

---

## Требования к моделям
- Модель должна реализовывать `IDocument` (Id, CreatedAt)

