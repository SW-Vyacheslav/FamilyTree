using FamilyTree.Domain.Entities.Content;
using FamilyTree.Domain.Entities.Privacy.Common;

namespace FamilyTree.Domain.Entities.Privacy
{
    public class ImagePrivacy : PrivacyEntity
    {
        public int Id { get; set; }

        public Image Image { get; set; }
    }
}
