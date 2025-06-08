using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitTwin.Core.Users.Logic
{
    /// <summary>
    /// Конфигурация пользователя
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Email).IsRequired().HasMaxLength(60);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Name).IsRequired(false).HasMaxLength(60);
            builder.Property(x => x.Password).IsRequired(false).HasMaxLength(255);
            builder.Property(x => x.Type).HasDefaultValue(UserTypeEnum.User);
            builder.Property(x => x.Status).HasDefaultValue(UserStatusEnum.NotActive);
        }
    }
}
