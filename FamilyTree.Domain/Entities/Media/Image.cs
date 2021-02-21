using FamilyTree.Domain.Common;

namespace FamilyTree.Domain.Entities.Media
{
    public class Image : AuditableEntity
    {
        public int Id { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageFormat { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}