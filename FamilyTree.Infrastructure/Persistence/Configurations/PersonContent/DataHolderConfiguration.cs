using FamilyTree.Domain.Entities.PersonContent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree.Infrastructure.Persistence.Configurations.PersonContent
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
                .HasColumnType("bit")
                .HasDefaultValueSql("1");
        }
    }
}
