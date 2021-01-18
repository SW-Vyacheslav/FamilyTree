using FamilyTree.Domain.Common;
using System.Collections.Generic;

namespace FamilyTree.Domain.Entities.Tree
{
    public class FamilyTreeEntity : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Person> People { get; set; }
    }
}
