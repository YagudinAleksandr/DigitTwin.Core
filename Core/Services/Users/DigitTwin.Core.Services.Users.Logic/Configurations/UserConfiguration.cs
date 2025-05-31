using DigitTwin.Infrastructure.DataContext;
using DigitTwin.Lib.Misc;
using Microsoft.EntityFrameworkCore;

namespace DigitTwin.Core.Services.Users
{
    /// <summary>
    /// Конфигурация пользователя
    /// </summary>
    internal class UserConfiguration : EntityConfiguration
    {
        public override string TableName => "Users";

        public override void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(TableName);
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Email).IsRequired().HasMaxLength(DefaultDatabaseParams.MIDDLE_TEXT_LENGTH);
                entity.Property(x => x.Name).IsRequired(false).HasMaxLength(DefaultDatabaseParams.MIDDLE_TEXT_LENGTH);
                entity.Property(x => x.Status).HasDefaultValue(UserStatusEnum.Inactive);
                entity.Property(x => x.Type).HasDefaultValue(UserTypeEnum.User);
                entity.Property(x => x.Password).IsRequired().HasMaxLength(DefaultDatabaseParams.STANDART_TEXT_LENGTH);
                entity.HasIndex(u => u.Email).IsUnique();
            });
        }
    }
}
