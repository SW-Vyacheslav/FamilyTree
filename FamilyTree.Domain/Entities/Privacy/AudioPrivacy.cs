using FamilyTree.Domain.Entities.Media;
using FamilyTree.Domain.Entities.Privacy.Common;

namespace FamilyTree.Domain.Entities.Privacy
{
    public class AudioPrivacy : PrivacyEntity
    {
        public int Id { get; set; }

        public int AudioId { get; set; }

        public Audio Audio { get; set; }
    }
}
