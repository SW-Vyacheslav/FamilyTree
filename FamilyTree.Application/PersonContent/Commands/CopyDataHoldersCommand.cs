using MediatR;
using System.Collections.Generic;

namespace FamilyTree.Application.PersonContent.Commands
{
    public class CopyDataHoldersCommand : IRequest
    {
        public List<int> DataHoldersIds { get; set; }

        public int DataBlockId { get; set; }

        public string UserId { get; set; }
    }
}
