using Microsoft.EntityFrameworkCore;

namespace DigitTwin.Infrastructure.DataContext
{
    /// <summary>
    /// Контекст подключения к MS SQL
    /// <inheritdoc cref="AppDbContext"/>
    /// </summary>
    public class MsSqlDbContext : AppDbContext
    {
        public MsSqlDbContext(
        DbContextOptions<MsSqlDbContext> options,
        IEnumerable<IEntityTypeConfiguration> configurations)
        : base(options, configurations)
        {
        }
    }
}
