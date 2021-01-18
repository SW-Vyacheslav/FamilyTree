using MediatR;

namespace FamilyTree.Application.FamilyTies.Queries
{
    public class GetFamilyTiesByPersonIdQuery : IRequest<string>
    {
        public string UserId { get; set; }

        public int PersonId { get; set; }

        public int WifeId { get; set; }
    }
}
