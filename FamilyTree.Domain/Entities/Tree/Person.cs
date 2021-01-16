using FamilyTree.Domain.Common;
using FamilyTree.Domain.Entities.Content;

namespace FamilyTree.Domain.Entities.Tree
{
    public class Person : AuditableEntity
    {
        public int Id { get; set; }

        public FamilyTreeEntity FamilyTree { get; set; }

        public Image AvatarImage { get; set; }
    }
}
