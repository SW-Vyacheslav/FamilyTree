﻿using MediatR;

namespace FamilyTree.Application.Media.Commands
{
    public class CreateVideoCommand : IRequest<int>
    {
        public int DataBlockId { get; set; }

        public string FilePath { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsUploadOnServer { get; set; }

        public string UserId { get; set; }
    }
}