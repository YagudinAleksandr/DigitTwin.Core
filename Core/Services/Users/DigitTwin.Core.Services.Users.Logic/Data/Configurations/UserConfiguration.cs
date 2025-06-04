using DigitTwin.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitTwin.Core.Services.Users
{
    /// <summary>
    /// Конфигурация пользователя
    /// </summary>
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(DefaultDatabaseParams.SHORT_STRING_LENGTH);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(DefaultDatabaseParams.NORMAL_STRING_LENGTH);
            builder.Property(x => x.Name).IsRequired(false).HasMaxLength(DefaultDatabaseParams.SHORT_STRING_LENGTH);
            builder.Property(x => x.Type).IsRequired().HasDefaultValue(UserTypeEnum.User);
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(UserStatusEnum.NotActive);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(DefaultDatabaseParams.SMALL_STRING_LENGTH);
        }
    }
}
