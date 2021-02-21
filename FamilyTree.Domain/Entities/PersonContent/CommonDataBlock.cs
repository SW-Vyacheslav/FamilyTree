using FamilyTree.Domain.Common;

namespace FamilyTree.Domain.Entities.PersonContent
{
    public class CommonDataBlock : AuditableEntity
    {
        public int Id { get; set; }

        public int DataBlockId { get; set; }

        public DataBlock DataBlock { get; set; }

        public int? DataCategoryId { get; set; }

        public DataCategory DataCategory { get; set; }
    }
}
