using FamilyTree.Domain.Entities.Privacy;
using FamilyTree.Domain.Enums.Privacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree.Infrastructure.Persistence.Configurations.Privacy
{
    public class AudioPrivacyConfiguration : IEntityTypeConfiguration<AudioPrivacy>
    {
        public void Configure(EntityTypeBuilder<AudioPrivacy> builder)
        {
            builder.HasKey(ap => ap.Id);

            builder.Property(ap => ap.PrivacyLevel)
                .HasDefaultValue(PrivacyLevel.Confidential);

            builder.Property(ap => ap.BeginDate)
                .HasColumnType("datetime2");

            builder.Property(ap => ap.EndDate)
                .HasColumnType("datetime2");

            builder.Property(ap => ap.IsAlways)
                .HasColumnType("bit")
                .HasDefaultValueSql("1");
        }
    }
}
