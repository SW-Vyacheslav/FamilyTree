using FamilyTree.Application.Media.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace FamilyTree.Application.Media.Queries
{
    public class GetVideosQuery : IRequest<List<VideoDto>>
    {
        public int DataBlockId { get; set; }

        public string UserId { get; set; }
    }
}
