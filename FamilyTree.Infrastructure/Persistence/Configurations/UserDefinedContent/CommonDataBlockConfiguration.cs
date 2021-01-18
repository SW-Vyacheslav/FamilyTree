using FamilyTree.Domain.Entities.UserDefinedContent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyTree.Infrastructure.Persistence.Configurations.UserDefinedContent
{
    public class CommonDataBlockConfiguration : IEntityTypeConfiguration<CommonDataBlock>
    {
        public void Configure(EntityTypeBuilder<CommonDataBlock> builder)
        {
            builder.HasKey(cdb => cdb.Id);
        }
    }
}
