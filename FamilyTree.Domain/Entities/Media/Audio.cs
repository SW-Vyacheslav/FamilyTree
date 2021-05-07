using FamilyTree.Domain.Common;

namespace FamilyTree.Domain.Entities.Media
{
    public class Audio : AuditableEntity
    {
        public int Id { get; set; }

        public string FilePath { get; set; }

        public string FileType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
