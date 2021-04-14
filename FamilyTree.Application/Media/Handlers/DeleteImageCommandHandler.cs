using FamilyTree.Application.Common.Exceptions;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.Media.Commands;
using FamilyTree.Domain.Entities.Media;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.Media.Handlers
{
    class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteImageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            Image image = await _context.Images
                .SingleOrDefaultAsync(v => v.CreatedBy.Equals(request.UserId) &&
                                           v.Id == request.Id,
                                      cancellationToken);

            if (image == null)
                throw new NotFoundException(nameof(Image), request.Id);

            _context.Images.Remove(image);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
