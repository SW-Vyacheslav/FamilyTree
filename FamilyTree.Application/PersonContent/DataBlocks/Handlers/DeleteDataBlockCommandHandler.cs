using FamilyTree.Application.Common.Exceptions;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.PersonContent.DataBlocks.Commands;
using FamilyTree.Domain.Entities.PersonContent;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.PersonContent.DataBlocks.Handlers
{
    public class DeleteDataBlockCommandHandler : IRequestHandler<DeleteDataBlockCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteDataBlockCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteDataBlockCommand request, CancellationToken cancellationToken)
        {
            DataBlock dataBlock = await _context.DataBlocks
                .SingleOrDefaultAsync(dh => dh.CreatedBy.Equals(request.UserId) &&
                                            dh.Id == request.Id,
                                      cancellationToken);

            if (dataBlock == null)
                throw new NotFoundException(nameof(DataHolder), request.Id);

            _context.DataBlocks.Remove(dataBlock);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
