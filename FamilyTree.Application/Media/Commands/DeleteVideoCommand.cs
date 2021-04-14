using MediatR;

namespace FamilyTree.Application.Media.Commands
{
    public class DeleteVideoCommand : IRequest
    {
        public int Id { get; set; }

        public string UserId { get; set; }
    }
}
