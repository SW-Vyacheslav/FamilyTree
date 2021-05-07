namespace FamilyTree.Domain.Entities.Identity
{
    public class Profile
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
