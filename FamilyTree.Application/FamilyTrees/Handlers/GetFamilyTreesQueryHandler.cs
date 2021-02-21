using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.FamilyTrees.Queries;
using FamilyTree.Application.FamilyTrees.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.FamilyTrees.Handlers
{
    public class GetFamilyTreesQueryHandler : IRequestHandler<GetFamilyTreesQuery, List<FamilyTreeEntityDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetFamilyTreesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FamilyTreeEntityDto>> Handle(GetFamilyTreesQuery request, CancellationToken cancellationToken)
        {
            List<FamilyTreeEntityDto> result = await _context.FamilyTrees
                .Where(dt => dt.CreatedBy.Equals(request.UserId))
                .Join(_context.FamilyTreesMainPeople,
                ft => ft.Id,
                mp => mp.FamilyTreeId,
                (ft, mp) => new FamilyTreeEntityDto()
                {
                    Id = ft.Id,
                    Name = ft.Name,
                    MainPersonId = mp.MainPersonId
                })
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
