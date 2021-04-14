using MediatR;
using System.Collections.Generic;

namespace FamilyTree.Application.Media.Commands
{
    public class CopyVideosCommand : IRequest
    {
        public List<int> VideosIds { get; set; }

        public int DataBlockId { get; set; }

        public string UserId { get; set; }
    }
}
