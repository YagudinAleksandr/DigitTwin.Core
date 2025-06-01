using Microsoft.EntityFrameworkCore;

namespace DigitTwin.Infrastructure.DataContext
{
    /// <summary>
    /// Конфигурация таблиц сущностей
    /// </summary>
    public interface IEntityTypeConfiguration
    {
        /// <summary>
        /// Конфигурация
        /// </summary>
        /// <param name="modelBuilder">Провайдер для построения конфигураций</param>
        void Configure(ModelBuilder modelBuilder);
    }
}
