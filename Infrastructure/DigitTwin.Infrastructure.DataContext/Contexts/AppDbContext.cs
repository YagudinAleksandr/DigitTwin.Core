using Microsoft.EntityFrameworkCore;

namespace DigitTwin.Infrastructure.DataContext
{
    /// <summary>
    /// Базовый контекст подключения к БД
    /// </summary>
    public abstract class AppDbContext : DbContext
    {
        /// <summary>
        /// Список конфигураций
        /// </summary>
        private readonly IEnumerable<IEntityTypeConfiguration> _configurations;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="options">Настройки</param>
        /// <param name="configurations">Конфигурации <see cref="IEntityTypeConfiguration"/></param>
        protected AppDbContext(
        DbContextOptions options,
        IEnumerable<IEntityTypeConfiguration> configurations)
        : base(options)
        {
            _configurations = configurations;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Применяем все динамически зарегистрированные конфигурации
            foreach (var configuration in _configurations)
            {
                configuration.Configure(modelBuilder);
            }
        }
    }
}
