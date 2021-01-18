using FamilyTree.Domain.Entities.Tree;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree.Infrastructure.Persistence.Configurations.Tree
{
    public class FamilyTreeMainPersonConfiguration : IEntityTypeConfiguration<FamilyTreeMainPerson>
    {
        public void Configure(EntityTypeBuilder<FamilyTreeMainPerson> builder)
        {
            builder.HasKey(f => f.Id);
        }
    }
}
