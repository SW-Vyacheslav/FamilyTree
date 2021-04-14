using FamilyTree.Domain.Entities.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree.Infrastructure.Persistence.Configurations.Media
{
    public class VideoConfiguration : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.FilePath)
                .HasColumnType("nvarchar(260)");

            builder.Property(v => v.Title)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(v => v.Description)
                .HasColumnType("nvarchar(1000)");

            builder.Property(v => v.PreviewImageData)
                .HasColumnType("image")
                .IsRequired();

            builder.Property(v => v.PreviewImageFormat)
                .HasColumnType("nvarchar(10)")
                .IsRequired();
        }
    }
}
