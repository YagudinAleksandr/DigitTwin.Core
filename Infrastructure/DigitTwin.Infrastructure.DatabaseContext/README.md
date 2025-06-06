# DigitTwin.Infrastructure.DatabaseContext
## Описание
Инфраструктурная библиотека для работы с базами данных.

Поддерживаемые типы баз данных: MS SQL, PostgreSQL

## Применение
### Настройка в проекте

1. Создать файл сущности
```csharp
public class {ENTITY_NAME}
    {
        public Guid Id { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username { get; set; } = null!;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Тип <see cref="UserTypeEnum"/>
        /// </summary>
        public UserTypeEnum Type { get; set; }

        /// <summary>
        /// Статус <see cref="UserStatusEnum"/>
        /// </summary>
        public UserStatusEnum Status { get; set; }
    }
```
`ENTITY_NAME` - название сущности. Например, `User`

2. Создать конфигурацию
```csharp
internal class {ENTITY_NAME}Configuration : IEntityTypeConfiguration<{ENTITY_NAME}>
    {
        public void Configure(EntityTypeBuilder<{ENTITY_NAME}> builder)
        {
            builder.ToTable("{TABLE_NAME}");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(DefaultDatabaseParams.SHORT_STRING_LENGTH);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(DefaultDatabaseParams.NORMAL_STRING_LENGTH);
            builder.Property(x => x.Name).IsRequired(false).HasMaxLength(DefaultDatabaseParams.SHORT_STRING_LENGTH);
            builder.Property(x => x.Type).IsRequired().HasDefaultValue(UserTypeEnum.User);
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(UserStatusEnum.NotActive);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(DefaultDatabaseParams.SMALL_STRING_LENGTH);
        }
    }
```
`TABLE_NAME` - название сущности. Например `Users`
`ENTITY_NAME` - название сущности. Например, `User`

3.В сервисе импользуемом данную библиотеку необходимо моздать файл контекста подключения к базе данных
```csharp
public class {TABLE_NAME}DbContext : ApplicationDbContext
    {
        public {TABLE_NAME}DbContext(DbContextOptions<{TABLE_NAME}DbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof({ENTITY_NAME}Configuration).Assembly);
        }
    }
```
`TABLE_NAME` - название сущности. Например `Users`
`ENTITY_NAME` - название сущности. Например, `User`

4. Создать фабрику контекстов
```csharp
internal class {TABLE_NAME}DbContextFactory : IDesignTimeDbContextFactory<{TABLE_NAME}DbContext>
    {
        public UsersDbContext CreateDbContext(string[] args)
        {
            // Создаем конфигурацию вручную
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var dbSettings = configuration.GetSection("DatabaseSettings");
            var dbType = Enum.Parse<DatabaseType>(dbSettings["Type"]!);
            var connectionString = dbSettings["ConnectionString"]!;

            var optionsBuilder = new DbContextOptionsBuilder<{TABLE_NAME}DbContext>();

            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    optionsBuilder.UseSqlServer(connectionString);
                    break;
                case DatabaseType.PostgreSQL:
                    optionsBuilder.UseNpgsql(connectionString);
                    break;
                default:
                    throw new ArgumentException("Unsupported database type");
            }

            return new {TABLE_NAME}DbContext(optionsBuilder.Options);
        }
    }
```
`TABLE_NAME` - название сущности. Например `Users`

5. Настроить DI
```csharp
var settings = configuration.GetSection("{SectionName}");
services.AddDatabaseContext<{TABLE_NAME}DbContext>(settings);
```
`SectionName` - секция конфигурации в файле appsettings.json. Например, `DatabaseSettings`
`TABLE_NAME` - название сущности. Например `Users`

6. Настроить подключение в appsettings.json
```json
"{SectionName}": {
   "Type": "{Type}",
   "ConnectionString": "{ConnectionString}"
}
```
`SectionName` - секция конфигурации в файле appsettings.json. Например, `DatabaseSettings`
`Type` - тип базы данных, `PostgreSQL` или `SqlServer`
`ConnectionString` - строка подключения к базе данных. Например, `Host=localhost;Port=5444;Database=DigitTwinUsers;Username=postgres;Password=postgres`

### Создание миграции через PM

```shel
Add-Migration UserInitMigration -Context {TABLE_NAME}DbContext -OutputDir {Dir} -StartupProject {StartupProject} -Project {Project}
```
`TABLE_NAME` - название сущности. Например `Users`
`Dir` - дмректория сохранения миграций, например, `Data\Migrations\Postgre`
`StartupProject` - запускающий проект, например, `DigitTwin.Core.Services.Users.Api`
`Project` - проект, гдебудут сохранены миграции, например, `DigitTwin.Core.Services.Users.Logic`

### Применение миграций
```shel
Update-Database -Context {TABLE_NAME}DbContext -OutputDir {Dir} -StartupProject {StartupProject} -Project {Project}
```
`Dir` - дмректория сохранения миграций, например, `Data\Migrations\Postgre`
`StartupProject` - запускающий проект, например, `DigitTwin.Core.Services.Users.Api`
`Project` - проект, гдебудут сохранены миграции, например, `DigitTwin.Core.Services.Users.Logic`