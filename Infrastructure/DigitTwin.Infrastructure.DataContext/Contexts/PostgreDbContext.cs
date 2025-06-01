using Microsoft.EntityFrameworkCore;

namespace DigitTwin.Infrastructure.DataContext
{
    /// <summary>
    /// Контекст подключения к PostgreSQL
    /// <inheritdoc cref="AppDbContext"/>
    /// </summary>
    public class PostgreDbContext : AppDbContext
    {
        public PostgreDbContext(
        DbContextOptions<PostgreDbContext> options,
        IEnumerable<IEntityTypeConfiguration> configurations)
        : base(options, configurations)
        {
        }
    }
}
