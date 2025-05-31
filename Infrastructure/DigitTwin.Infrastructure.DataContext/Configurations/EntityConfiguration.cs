using Microsoft.EntityFrameworkCore;

namespace DigitTwin.Infrastructure.DataContext
{
    /// <summary>
    /// Конфигуратор моделей
    /// </summary>
    public abstract class EntityConfiguration
    {
        /// <summary>
        /// Название таблицы
        /// </summary>
        public abstract string TableName { get; }

        /// <summary>
        /// Создание конфигураций
        /// </summary>
        /// <param name="modelBuilder"><see cref="ModelBuilder"/></param>
        public abstract void Configure(ModelBuilder modelBuilder);
    }
}
