using DigitTwin.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DigitTwin.Core.Users.Logic.Data
{
    public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            // Конфигурация для времени разработки
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var settings = configuration.GetSection(DatabaseSettings.SectionName).Get<DatabaseSettings>();
            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();

            if (settings.Provider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
            {
                optionsBuilder.UseNpgsql(settings.ConnectionStrings.PostgreSQL);
            }
            else if (settings.Provider.Equals("MSSQL", StringComparison.OrdinalIgnoreCase))
            {
                optionsBuilder.UseSqlServer(settings.ConnectionStrings.MSSQL);
            }
            else
            {
                throw new InvalidOperationException($"Unsupported database provider: {settings.Provider}");
            }

            return new UserDbContext(optionsBuilder.Options);
        }
    }
}
