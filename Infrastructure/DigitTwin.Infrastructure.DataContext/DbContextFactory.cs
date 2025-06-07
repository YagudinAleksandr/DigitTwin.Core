using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DigitTwin.Infrastructure.DataContext
{
    public class DbContextFactory<TContext> : IDbContextFactory<TContext>
        where TContext : DbContext
    {
        #region CTOR
        /// <inheritdoc cref="DatabaseSettings"/>
        private readonly DatabaseSettings _settings;

        /// <inheritdoc cref="IServiceProvider"/>
        private readonly IServiceProvider _serviceProvider;

        public DbContextFactory(IOptions<DatabaseSettings> settings,
            IServiceProvider serviceProvider)
        {
            _settings = settings.Value;
            _serviceProvider = serviceProvider;
        }

        #endregion

        public DbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            ConfigureDatabaseProvider(optionsBuilder);

            // Создание контекста с возможностью внедрения зависимостей
            return ActivatorUtilities.CreateInstance<TContext>(
                _serviceProvider,
                optionsBuilder.Options
            );
        }

        /// <summary>
        /// Конфигурирование провайдера базы данных
        /// </summary>
        /// <param name="builder">Настройки</param>
        /// <exception cref="InvalidOperationException">Неверный провайдер</exception>
        private void ConfigureDatabaseProvider(DbContextOptionsBuilder<TContext> builder)
        {
            var provider = _settings.Provider.ToUpperInvariant();
            var connectionString = provider switch
            {
                "POSTGRESQL" => _settings.ConnectionStrings.PostgreSQL,
                "MSSQL" => _settings.ConnectionStrings.MSSQL,
                _ => throw new InvalidOperationException($"Unsupported provider: {_settings.Provider}")
            };

            switch (provider)
            {
                case "POSTGRESQL":
                    builder.UseNpgsql(connectionString);
                    break;
                case "MSSQL":
                    builder.UseSqlServer(connectionString);
                    break;
            }
        }
    }
}
