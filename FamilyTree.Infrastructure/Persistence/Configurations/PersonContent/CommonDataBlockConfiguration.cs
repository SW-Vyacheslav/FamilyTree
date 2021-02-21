using FamilyTree.Domain.Entities.PersonContent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree.Infrastructure.Persistence.Configurations.PersonContent
{
    public class CommonDataBlockConfiguration : IEntityTypeConfiguration<CommonDataBlock>
    {
        public void Configure(EntityTypeBuilder<CommonDataBlock> builder)
        {
            builder.HasKey(cdb => cdb.Id);
        }
    }
}
