﻿using FamilyTree.Domain.Enums.PersonContent;
using MediatR;

namespace FamilyTree.Application.PersonContent.Commands
{
    public class CreateDataHolderCommand : IRequest<int>
    {
        public DataHolderType DataHolderType { get; set; }

        public string Title { get; set; }

        public string Data { get; set; }

        public int DataBlockId { get; set; }

        public string UserId { get; set; }
    }
}
