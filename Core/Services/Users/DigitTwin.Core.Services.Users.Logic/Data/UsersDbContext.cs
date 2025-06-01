using DigitTwin.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DigitTwin.Core.Services.Users.Logic
{
    /// <summary>
    /// Контекс пользователей
    /// </summary>
    public class UsersDbContext : ApplicationDbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }
    }
}
