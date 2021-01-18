using FamilyTree.Domain.Entities.UserDefinedContent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree.Infrastructure.Persistence.Configurations.UserDefinedContent
{
    public class DataBlockConfiguration : IEntityTypeConfiguration<DataBlock>
    {
        public void Configure(EntityTypeBuilder<DataBlock> builder)
        {
            builder.HasKey(db => db.Id);

            builder.Property(db => db.Title)
                .HasColumnType("nvarchar(50)")
                .IsRequired();
        }
    }
}
