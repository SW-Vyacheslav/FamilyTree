using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.FamilyTrees.Queries;
using FamilyTree.Application.FamilyTrees.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.FamilyTrees.Handlers
{
    public class GetFamilyTreeByIdQueryHandler : IRequestHandler<GetFamilyTreeByIdQuery, FamilyTreeVm>
    {
        private readonly IApplicationDbContext _dataContext;

        public GetFamilyTreeByIdQueryHandler(IApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<FamilyTreeVm> Handle(GetFamilyTreeByIdQuery request, CancellationToken cancellationToken)
        {
            FamilyTreeVm result = new FamilyTreeVm();



            return result;
        }
    }
}
