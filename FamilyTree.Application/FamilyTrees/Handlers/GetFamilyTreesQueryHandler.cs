using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.FamilyTrees.Queries;
using FamilyTree.Application.FamilyTrees.ViewModels;
using FamilyTree.Domain.Entities.Tree;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.FamilyTrees.Handlers
{
    public class GetFamilyTreesQueryHandler : IRequestHandler<GetFamilyTreesQuery, List<FamilyTreeEntityVm>>
    {
        private readonly IApplicationDbContext _dataContext;

        public GetFamilyTreesQueryHandler(IApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<FamilyTreeEntityVm>> Handle(GetFamilyTreesQuery request, CancellationToken cancellationToken)
        {
            List<FamilyTreeEntityVm> result = new List<FamilyTreeEntityVm>();
            List<FamilyTreeEntity> familyTrees = await _dataContext.FamilyTrees
                .Include(ft => ft.MainPerson)
                .Where(dt => dt.CreatedBy.Equals(request.UserId))
                .ToListAsync();

            if (familyTrees != null)
            {
                result = familyTrees
                    .Select(ToVm)
                    .ToList();
            }

            return result;
        }

        private FamilyTreeEntityVm ToVm(FamilyTreeEntity familyTreeEntity)
        {
            return new FamilyTreeEntityVm()
            {
                Id = familyTreeEntity.Id,
                Name = familyTreeEntity.Name,
                MainPersonId = familyTreeEntity.MainPerson.Id
            };
        }
    }
}
