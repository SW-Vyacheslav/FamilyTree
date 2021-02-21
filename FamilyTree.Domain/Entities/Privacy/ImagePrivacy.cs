using FamilyTree.Domain.Entities.Media;
using FamilyTree.Domain.Entities.Privacy.Common;

namespace FamilyTree.Domain.Entities.Privacy
{
    public class ImagePrivacy : PrivacyEntity
    {
        public int Id { get; set; }

        public int ImageId { get; set; }

        public Image Image { get; set; }
    }
}
