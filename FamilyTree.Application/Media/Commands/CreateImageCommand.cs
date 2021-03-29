﻿using MediatR;

namespace FamilyTree.Application.Media.Commands
{
    public class CreateImageCommand : IRequest<int>
    {
        public int DataBlockId { get; set; }

        public string ImageData { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }
    }
}
