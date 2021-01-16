using FamilyTree.Domain.Entities.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree.Infrastructure.Persistence.Configurations.Content
{
    public class VideoConfiguration : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.FilePath)
                .HasColumnType("nvarchar(260)");

            builder.Property(i => i.Title)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(i => i.Description)
                .HasColumnType("nvarchar(1000)");
        }
    }
}
