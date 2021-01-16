using FamilyTree.Domain.Entities.Privacy;
using FamilyTree.Domain.Enums.Privacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree.Infrastructure.Persistence.Configurations.Privacy
{
    public class ImagePrivacyConfiguration : IEntityTypeConfiguration<ImagePrivacy>
    {
        public void Configure(EntityTypeBuilder<ImagePrivacy> builder)
        {
            builder.HasKey(ip => ip.Id);

            builder.Property(ip => ip.Image)
                .IsRequired();

            builder.Property(ip => ip.PrivacyLevel)
                .HasDefaultValue(PrivacyLevel.Confidential);

            builder.Property(ip => ip.BeginDate)
                .HasColumnType("datetime2");

            builder.Property(ip => ip.EndDate)
                .HasColumnType("datetime2");

            builder.Property(ip => ip.IsAlways)
                .HasDefaultValue(true);
        }
    }
}
