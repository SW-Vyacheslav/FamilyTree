using FamilyTree.Domain.Entities.Privacy;
using FamilyTree.Domain.Enums.Privacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree.Infrastructure.Persistence.Configurations.Privacy
{
    public class VideoPrivacyConfiguration : IEntityTypeConfiguration<VideoPrivacy>
    {
        public void Configure(EntityTypeBuilder<VideoPrivacy> builder)
        {
            builder.HasKey(vp => vp.Id);

            builder.Property(vp => vp.Video)
                .IsRequired();

            builder.Property(vp => vp.PrivacyLevel)
                .HasDefaultValue(PrivacyLevel.Confidential);

            builder.Property(vp => vp.BeginDate)
                .HasColumnType("datetime2");

            builder.Property(vp => vp.EndDate)
                .HasColumnType("datetime2");

            builder.Property(vp => vp.IsAlways)
                .HasDefaultValue(true);
        }
    }
}
