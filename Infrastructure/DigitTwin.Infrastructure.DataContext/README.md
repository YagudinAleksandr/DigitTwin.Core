# DigitTwin Data Context
## �������� ������
������ ������ ������������� ��� ������ � ���������� ��� ������. �������������� ���� ������:
- PostgreSQL
- MS SQL

## ��������� ������� ���������
��������� `appsttings.json`
```json
{
  "Database": {
    "DbType": "NPG", // ��� "MSQ" ��� SQL Server
    "ConnectionString": "���� ������ �����������"
  }
}
```

## ������������� � ������
### ����������� ������� (Program.cs)
```csharp
ar builder = WebApplication.CreateBuilder(args);

// ����������� EF Core � �������������� ����������
builder.Services.AddCoreEf(builder.Configuration);
```

### ���������� �������� ��� �������
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
        logger.LogError(ex, "������ ��� ���������� ��������");
    }
}

app.Run();
```

## ����������� ������� � ������������
### 1. �������� �������� � ������
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
### 2. �������� ������������ ��������
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

### 3. ��������������� ������ � ��������������
```csharp
builder.Services.AddCoreEf(
    builder.Configuration,
    configurationsAssembly: typeof(Program).Assembly);
```

## ������ � ����������
### �������� ��������
```bash
# ��� PostgreSQL
dotnet ef migrations add InitialCreate --context PostgreDbContext --output-dir "Migrations/Postgre"
```

```bash
# ��� SQL Server
dotnet ef migrations add InitialCreate --context MsSqlDbContext --output-dir "Migrations/MsSql"
```

### ���������� ��������
```bash
# ��� PostgreSQL
dotnet ef database update --context PostgreDbContext
```

```bash
# ��� SQL Server
dotnet ef database update --context MsSqlDbContext
```