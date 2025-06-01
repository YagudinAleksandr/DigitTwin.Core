using Microsoft.EntityFrameworkCore;

namespace DigitTwin.Infrastructure.DatabaseContext
{
    /// <summary>
    /// Шаблон контекста
    /// </summary>
    public abstract class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
