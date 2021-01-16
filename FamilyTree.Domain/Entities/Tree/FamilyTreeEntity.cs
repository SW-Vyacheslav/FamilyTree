using FamilyTree.Domain.Common;

namespace FamilyTree.Domain.Entities.Tree
{
    public class FamilyTreeEntity : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Person MainPerson { get; set; }
    }
}
