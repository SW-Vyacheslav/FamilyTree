using FamilyTree.Domain.Entities.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree.Infrastructure.Persistence.Configurations.Content
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.ImageData)
                .HasColumnType("image");

            builder.Property(i => i.Title)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(i => i.Description)
                .HasColumnType("nvarchar(1000)");
        }
    }
}
