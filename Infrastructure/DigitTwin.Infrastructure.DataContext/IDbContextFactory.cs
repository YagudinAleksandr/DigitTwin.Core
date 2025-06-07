using Microsoft.EntityFrameworkCore;

namespace DigitTwin.Infrastructure.DataContext
{
    /// <summary>
    /// Контекст подключения к базамданных
    /// </summary>
    /// <typeparam name="TContext">Контекст</typeparam>
    public interface IDbContextFactory<out TContext> where TContext : DbContext
    {
        /// <summary>
        /// Создание контекста
        /// </summary>
        /// <returns>Контекст подключения к базам данных</returns>
        DbContext CreateDbContext();
    }
}
