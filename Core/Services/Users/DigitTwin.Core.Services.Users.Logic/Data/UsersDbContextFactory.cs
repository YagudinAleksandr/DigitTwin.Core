using DigitTwin.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DigitTwin.Core.Services.Users
{
    /// <summary>
    /// Фабрика контекстов 
    /// <inheritdoc cref="IDesignTimeDbContextFactory{TContext}"/>
    /// </summary>
    internal class UsersDbContextFactory : IDesignTimeDbContextFactory<UsersDbContext>
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

            var optionsBuilder = new DbContextOptionsBuilder<UsersDbContext>();

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

            return new UsersDbContext(optionsBuilder.Options);
        }
    }
}
