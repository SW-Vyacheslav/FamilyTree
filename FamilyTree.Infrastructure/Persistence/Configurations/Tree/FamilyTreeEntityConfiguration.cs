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

            builder.Property(t => t.UserId)
                .HasColumnType("nvarchar(450)")
                .IsRequired();

            builder.HasOne(ft => ft.User)
                .WithMany(u => u.FamilyTrees)
                .HasForeignKey(ft => ft.UserId);

            builder.ToTable("FamilyTrees");
        }
    }
}
