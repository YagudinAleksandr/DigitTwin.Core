using Microsoft.EntityFrameworkCore;

namespace DigitTwin.Core.Users.Logic.Data
{
    /// <summary>
    /// Контекст сервиса пользователей
    /// </summary>
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
