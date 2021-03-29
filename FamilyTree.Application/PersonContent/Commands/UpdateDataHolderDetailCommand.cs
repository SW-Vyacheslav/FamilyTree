using MediatR;

namespace FamilyTree.Application.PersonContent.Commands
{
    public class UpdateDataHolderDetailCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string UserId { get; set; }
    }
}
