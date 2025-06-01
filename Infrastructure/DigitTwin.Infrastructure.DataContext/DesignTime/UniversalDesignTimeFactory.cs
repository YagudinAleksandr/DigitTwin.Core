using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DigitTwin.Infrastructure.DataContext
{
    /// <summary>
    /// Универсальная фабрика подключения к БД
    /// </summary>
    public class UniversalDesignTimeFactory
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Поиск appsettings.json в возможных местах
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
            if (!Directory.Exists(Path.Combine(basePath, "bin")))
            {
                basePath = Directory.GetCurrentDirectory();
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var dbOptions = new DatabaseOptions();
            configuration.GetSection("Database").Bind(dbOptions);

            // Фолбэк значениями по умолчанию
            dbOptions.DbType ??= "NPG";
            dbOptions.ConnectionString ??= "Host=localhost;Database=defaultdb;Username=user;Password=pass";

            // Создаем пустые конфигурации для Design Time
            var configurations = new List<IEntityTypeConfiguration>();

            // Создаем контекст
            switch (dbOptions.DbType.ToUpper())
            {
                case "NPG":
                    return new PostgreDbContext(
                        new DbContextOptionsBuilder<PostgreDbContext>()
                            .UseNpgsql(dbOptions.ConnectionString)
                            .Options,
                        configurations);

                case "MSQ":
                    return new MsSqlDbContext(
                        new DbContextOptionsBuilder<MsSqlDbContext>()
                            .UseSqlServer(dbOptions.ConnectionString)
                            .Options,
                        configurations);

                default:
                    throw new InvalidOperationException($"Invalid database type: {dbOptions.DbType}");
            }
        }
    }
}
