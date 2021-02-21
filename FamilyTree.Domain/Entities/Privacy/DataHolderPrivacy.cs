using FamilyTree.Domain.Entities.Privacy.Common;
using FamilyTree.Domain.Entities.PersonContent;

namespace FamilyTree.Domain.Entities.Privacy
{
    public class DataHolderPrivacy : PrivacyEntity
    {
        public int Id { get; set; }

        public int DataHolderId { get; set; }

        public DataHolder DataHolder { get; set; }
    }
}
