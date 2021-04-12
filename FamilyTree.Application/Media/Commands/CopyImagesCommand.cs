using MediatR;
using System.Collections.Generic;

namespace FamilyTree.Application.Media.Commands
{
    public class CopyImagesCommand : IRequest
    {
        public List<int> ImagesIds { get; set; }

        public int DataBlockId { get; set; }

        public string UserId { get; set; }
    }
}
