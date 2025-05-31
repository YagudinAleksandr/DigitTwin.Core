using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DigitTwin.Infrastructure.DataContext
{
    /// <summary>
    /// Базовый контекс подключения к БД
    /// </summary>
    public abstract class DigitTwingMainContext : DbContext
    {
        protected DigitTwingMainContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Применяем все зарегистрированные конфигурации
            var configurations = this.GetService<IEnumerable<EntityConfiguration>>();
            foreach (var config in configurations)
            {
                config.Configure(modelBuilder);
            }
        }
    }
}
