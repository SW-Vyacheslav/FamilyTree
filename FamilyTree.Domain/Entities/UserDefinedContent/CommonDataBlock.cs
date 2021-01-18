using FamilyTree.Domain.Common;

namespace FamilyTree.Domain.Entities.UserDefinedContent
{
    public class CommonDataBlock : AuditableEntity
    {
        public int Id { get; set; }

        public int DataBlockId { get; set; }

        public DataBlock DataBlock { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
