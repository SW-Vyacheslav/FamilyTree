using FamilyTree.Application.FamilyTrees.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace FamilyTree.Application.FamilyTrees.Queries
{
    public class GetAllFamilyTreesQuery : IRequest<List<FamilyTreeEntityVm>>
    {
        public string UserId { get; set; }
    }
}
