using FamilyTree.Domain.Entities.UserDefinedContent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree.Infrastructure.Persistence.Configurations.UserDefinedContent
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(c => c.IsDeletable)
                .HasDefaultValue(true);

            builder.Property(c => c.Person)
                .IsRequired();
        }
    }
}
