using FamilyTree.Application.Common.Exceptions;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.PersonContent.Commands;
using FamilyTree.Domain.Entities.PersonContent;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.PersonContent.Handlers
{
    public class CreateDataBlockCommandHandler : IRequestHandler<CreateDataBlockCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateDataBlockCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateDataBlockCommand request, CancellationToken cancellationToken)
        {
            DataCategory dataCategory = await _context.DataCategories
                .Include(dc => dc.DataBlocks)
                .SingleOrDefaultAsync(dc => dc.CreatedBy.Equals(request.UserId) &&
                                            dc.Id == request.DataCategoryId, 
                                      cancellationToken);

            if (dataCategory == null)
                throw new NotFoundException(nameof(DataCategory), request.DataCategoryId);

            DataBlock entity = new DataBlock();
            entity.DataCategoryId = dataCategory.Id;
            entity.Title = request.Title;
            entity.OrderNumber = dataCategory.DataBlocks.Count() + 1;

            _context.DataBlocks.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
