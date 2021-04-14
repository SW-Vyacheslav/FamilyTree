using FamilyTree.Application.Media.ViewModels;
using MediatR;

namespace FamilyTree.Application.Media.Queries
{
    public class GetVideoQuery : IRequest<VideoVm>
    {
        public int Id { get; set; }

        public string UserId { get; set; }
    }
}
