# DigitTwin Data Context
## Описание сборки
Данная сборка предназначена для работы с контекстом баз данных. Поддерживаемые базы данных:
- PostgreSQL
- MS SQL

## Настройка данного контекста
Настройка `appsttings.json`
```json
{
  "Database": {
    "DbType": "NPG", // или "MSQ" для SQL Server
    "ConnectionString": "Ваша строка подключения"
  }
}
```

## Использование в сборке
### Регистрация сервиса (Program.cs)
```csharp
ar builder = WebApplication.CreateBuilder(args);

// Регистрация EF Core с автоматической настройкой
builder.Services.AddCoreEf(builder.Configuration);
```

### Применение миграций при запуске
```csharp
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    try
    {
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ошибка при применении миграций");
    }
}

app.Run();
```

## Определение моделей и конфигураций
### 1. Создайте сущности в сборке
```csharp
// Entities/User.cs
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
```
### 2. Создайте конфигурацию сущности
```csharp
// Configurations/UserConfiguration.cs
public class UserConfiguration : EntityConfiguration
{
    public override void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(u => u.Id);
            
            entity.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);
                
            entity.HasIndex(u => u.Email)
                .IsUnique();
                
            entity.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }
}
```

### 3. Зарегистрируйте сборку с конфигурациями
```csharp
builder.Services.AddCoreEf(
    builder.Configuration,
    configurationsAssembly: typeof(Program).Assembly);
```

## Работа с миграциями
### Создание миграций
```bash
# Для PostgreSQL
dotnet ef migrations add InitialCreate --context PostgreDbContext --output-dir "Migrations/Postgre"
```

```bash
# Для SQL Server
dotnet ef migrations add InitialCreate --context MsSqlDbContext --output-dir "Migrations/MsSql"
```

### Применение миграций
```bash
# Для PostgreSQL
dotnet ef database update --context PostgreDbContext
```

```bash
# Для SQL Server
dotnet ef database update --context MsSqlDbContext
```