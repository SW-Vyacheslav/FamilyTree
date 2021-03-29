﻿using MediatR;

namespace FamilyTree.Application.PersonContent.Commands
{
    public class UpdateDataHolderCommand : IRequest
    {
        public int Id { get; set; }

        public string Data { get; set; }

        public string UserId { get; set; }
    }
}
