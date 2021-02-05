using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.FamilyTrees.Queries;
using FamilyTree.Application.FamilyTrees.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.FamilyTrees.Handlers
{
    public class GetBloodTreeByIdQueryHandler : IRequestHandler<GetBloodTreeByIdQuery, BloodTreeVm>
    {
        private readonly IFamilyTreeService _service;

        public GetBloodTreeByIdQueryHandler(IFamilyTreeService service)
        {
            _service = service;
        }

        public async Task<BloodTreeVm> Handle(GetBloodTreeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetBloodTreeById(request, cancellationToken);
        }
    }
}
