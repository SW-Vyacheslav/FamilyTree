using FamilyTree.Domain.Entities.Media;
using FamilyTree.Domain.Entities.Privacy.Common;

namespace FamilyTree.Domain.Entities.Privacy
{
    public class VideoPrivacy : PrivacyEntity
    {
        public int Id { get; set; }

        public int VideoId { get; set; }

        public Video Video { get; set; }
    }
}
