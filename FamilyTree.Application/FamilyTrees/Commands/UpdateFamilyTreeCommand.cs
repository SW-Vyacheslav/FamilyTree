using MediatR;

namespace FamilyTree.Application.FamilyTrees.Commands
{
    public class UpdateFamilyTreeCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }
    }
}
