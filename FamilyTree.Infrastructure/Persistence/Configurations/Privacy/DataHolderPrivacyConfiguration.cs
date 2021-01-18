using FamilyTree.Domain.Entities.Privacy;
using FamilyTree.Domain.Enums.Privacy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree.Infrastructure.Persistence.Configurations.Privacy
{
    public class DataHolderPrivacyConfiguration : IEntityTypeConfiguration<DataHolderPrivacy>
    {
        public void Configure(EntityTypeBuilder<DataHolderPrivacy> builder)
        {
            builder.HasKey(dhp => dhp.Id);

            builder.Property(dhp => dhp.PrivacyLevel)
                .HasDefaultValue(PrivacyLevel.Confidential);

            builder.Property(dhp => dhp.BeginDate)
                .HasColumnType("datetime2");

            builder.Property(dhp => dhp.EndDate)
                .HasColumnType("datetime2");

            builder.Property(dhp => dhp.IsAlways)
                .HasColumnType("bit")
                .HasDefaultValueSql("1");
        }
    }
}
