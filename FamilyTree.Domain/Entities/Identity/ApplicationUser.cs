using FamilyTree.Domain.Entities.Tree;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FamilyTree.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public Profile Profile { get; set; }

        public ICollection<FamilyTreeEntity> FamilyTrees { get; set; }
    }
}
