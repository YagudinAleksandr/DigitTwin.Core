using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Конфигурация организации
    /// </summary>
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("Organizations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Inn).IsRequired().HasMaxLength(20);
            builder.HasIndex(x => x.Inn).IsUnique();
            builder.Property(x => x.Address).IsRequired().HasMaxLength(500);
            builder.Property(x => x.FactAddress).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Account).IsRequired().HasMaxLength(30);
            builder.Property(x => x.CorrespondentialAccount).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Ogrn).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Kpp).IsRequired().HasMaxLength(20);
            builder.Property(x => x.ShortName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(255);
            builder.HasMany(x => x.Users).WithOne(x => x.Organization).HasForeignKey(x => x.OrganizationId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
