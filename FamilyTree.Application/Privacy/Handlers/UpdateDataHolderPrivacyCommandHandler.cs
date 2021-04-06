using FamilyTree.Application.Common.Exceptions;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.Privacy.Commands;
using FamilyTree.Domain.Entities.Privacy;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.Privacy.Handlers
{
    public class UpdateDataHolderPrivacyCommandHandler : IRequestHandler<UpdateDataHolderPrivacyCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateDataHolderPrivacyCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateDataHolderPrivacyCommand request, CancellationToken cancellationToken)
        {
            DataHolderPrivacy privacy = await _context.DataHolderPrivacies
                .SingleOrDefaultAsync(p => p.CreatedBy.Equals(request.UserId) &&
                                           p.DataHolderId == request.Id,
                                      cancellationToken);

            if (privacy == null)
                throw new NotFoundException(nameof(DataHolderPrivacy), request.Id);

            if (!request.IsAlways)
            {
                privacy.BeginDate = request.BeginDate;
                privacy.EndDate = request.EndDate;
            }

            privacy.IsAlways = request.IsAlways;
            privacy.PrivacyLevel = request.PrivacyLevel;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
