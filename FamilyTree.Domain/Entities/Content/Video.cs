using FamilyTree.Domain.Common;

namespace FamilyTree.Domain.Entities.Content
{
    public class Video : AuditableEntity
    {
        public int Id { get; set; }

        public string FilePath { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}