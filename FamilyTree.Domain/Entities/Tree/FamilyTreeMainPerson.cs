namespace FamilyTree.Domain.Entities.Tree
{
    public class FamilyTreeMainPerson
    {
        public int Id { get; set; }

        public int? MainPersonId { get; set; }

        public Person MainPerson { get; set; }

        public int FamilyTreeId { get; set; }

        public FamilyTreeEntity FamilyTree { get; set; }
    }
}
