﻿using FamilyTree.Application.Media.Images.ViewModels;
using MediatR;

namespace FamilyTree.Application.Media.Images.Queries
{
    public class GetImageQuery : IRequest<ImageDto>
    {
        public int Id { get; set; }

        public string UserId { get; set; }
    }
}
