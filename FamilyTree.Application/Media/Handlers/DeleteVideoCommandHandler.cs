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
    public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteVideoCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            Video video = await _context.Videos
                .SingleOrDefaultAsync(v => v.CreatedBy.Equals(request.UserId) &&
                                           v.Id == request.Id,
                                      cancellationToken);

            if (video == null)
                throw new NotFoundException(nameof(Video), request.Id);

            _context.Videos.Remove(video);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
