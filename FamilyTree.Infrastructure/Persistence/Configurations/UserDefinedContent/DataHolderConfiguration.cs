using FamilyTree.Domain.Entities.UserDefinedContent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree.Infrastructure.Persistence.Configurations.UserDefinedContent
{
    public class DataHolderConfiguration : IEntityTypeConfiguration<DataHolder>
    {
        public void Configure(EntityTypeBuilder<DataHolder> builder)
        {
            builder.HasKey(dh => dh.Id);

            builder.Property(dh => dh.Title)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(dh => dh.Data)
                .HasColumnType("nvarchar(max)");

            builder.Property(dh => dh.IsDeletable)
                .HasDefaultValue(true);

            builder.Property(dh => dh.DataBlock)
                .IsRequired();
        }
    }
}
