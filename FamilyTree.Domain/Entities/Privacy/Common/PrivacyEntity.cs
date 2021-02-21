using FamilyTree.Domain.Common;
using FamilyTree.Domain.Enums.Privacy;
using System;

namespace FamilyTree.Domain.Entities.Privacy.Common
{
    public abstract class PrivacyEntity : AuditableEntity
    {
        public PrivacyLevel PrivacyLevel { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool? IsAlways { get; set; }
    }
}
