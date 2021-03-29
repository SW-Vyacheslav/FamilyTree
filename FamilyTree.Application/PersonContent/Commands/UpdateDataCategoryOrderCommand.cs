using MediatR;

namespace FamilyTree.Application.PersonContent.Commands
{
    public class UpdateDataCategoryOrderCommand : IRequest
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public string UserId { get; set; }
    }
}
