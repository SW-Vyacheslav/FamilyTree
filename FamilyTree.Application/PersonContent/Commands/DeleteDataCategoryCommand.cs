using MediatR;

namespace FamilyTree.Application.PersonContent.Commands
{
    public class DeleteDataCategoryCommand : IRequest
    {
        public int Id { get; set; }

        public string UserId { get; set; }
    }
}
