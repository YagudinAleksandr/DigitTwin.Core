# Провайдер для работы с реляционными базами данных
## Поддерживаемые провайдеры
- PostgreSQL
- MS SQL

## Использование в проекте
### Пример конфигурации пользователя (в проекте-потребителе)
```csharp
// UserConfiguration.cs
using Core.Ef.Configuration;
using DigitTwin.Lib.Misc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Your.Consumer.Project;

public class UserConfiguration : IEntityTypeConfiguration
{
    private readonly ILogger<UserConfiguration> _logger;

    public UserConfiguration(ILogger<UserConfiguration> logger)
    {
        _logger = logger;
    }

    public void Configure(ModelBuilder modelBuilder)
    {
        try
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(DefaultDatabaseParams.MIDDLE_TEXT_LENGTH);
                entity.Property(x => x.Name)
                    .IsRequired(false)
                    .HasMaxLength(DefaultDatabaseParams.MIDDLE_TEXT_LENGTH);
                entity.Property(x => x.Status)
                    .HasDefaultValue(UserStatusEnum.Inactive);
                entity.Property(x => x.Type)
                    .HasDefaultValue(UserTypeEnum.User);
                entity.Property(x => x.Password)
                    .IsRequired()
                    .HasMaxLength(DefaultDatabaseParams.STANDART_TEXT_LENGTH);
                entity.HasIndex(u => u.Email).IsUnique();
            });
            
            _logger.LogInformation("User configuration applied successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error applying user configuration");
            throw;
        }
    }
}
```

### Регистрация в Program.cs
```csharp
using Core.Ef.Extensions;
using Your.Consumer.Project;

var builder = WebApplication.CreateBuilder(args);

// Регистрация EF Core с динамическими конфигурациями
builder.Services.AddCoreEf(
    builder.Configuration,
    configurationsAssembly: typeof(UserConfiguration).Assembly);

var app = builder.Build();

// Применение миграций при запуске
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
```

### Пример использования в сервисе
```csharp
public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUserAsync(string name, string email, string password)
    {
        var user = new User 
        { 
            Name = name, 
            Email = email, 
            Password = password 
        };
        
        _context.Set<User>().Add(user);
        await _context.SaveChangesAsync();
        
        return user;
    }
}
```

### Создание миграций
- Для PostgreSQL
```shel
Add-Migration UserInitMigration `
  -Context PostgreDbContext `
  -OutputDirectory "Migrations\Postgre" `
  -Project Core.Ef `
  -StartupProject Your.Consumer.Project
```
- Для MS SQL
```shel
Add-Migration UserInitMigration `
  -Context MsSqlDbContext `
  -OutputDirectory "Migrations\MsSql" `
  -Project Core.Ef `
  -StartupProject Your.Consumer.Project
```