using Microsoft.EntityFrameworkCore;
using FamilyTree.Domain.Entities.Tree;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree.Infrastructure.Persistence.Configurations.Tree
{
    public class FamilyTreeEntityConfiguration : IEntityTypeConfiguration<FamilyTreeEntity>
    {
        public void Configure(EntityTypeBuilder<FamilyTreeEntity> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.ToTable("FamilyTrees");
        }
    }
}
