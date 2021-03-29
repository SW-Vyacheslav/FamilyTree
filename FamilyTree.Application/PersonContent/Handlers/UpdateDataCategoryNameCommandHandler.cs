using FamilyTree.Application.Common.Exceptions;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.PersonContent.Commands;
using FamilyTree.Domain.Entities.PersonContent;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.PersonContent.Handlers
{
    public class UpdateDataCategoryNameCommandHandler : IRequestHandler<UpdateDataCategoryNameCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateDataCategoryNameCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateDataCategoryNameCommand request, CancellationToken cancellationToken)
        {
            var dataCategory = await _context.DataCategories
                .SingleOrDefaultAsync(db => db.CreatedBy.Equals(request.UserId) &&
                                            db.Id == request.Id,
                                      cancellationToken);

            if (dataCategory == null)
                throw new NotFoundException(nameof(DataCategory), request.Id);

            dataCategory.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
