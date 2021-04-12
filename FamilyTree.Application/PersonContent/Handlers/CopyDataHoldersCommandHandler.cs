using FamilyTree.Application.Common.Exceptions;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.PersonContent.Commands;
using FamilyTree.Domain.Entities.PersonContent;
using FamilyTree.Domain.Entities.Privacy;
using FamilyTree.Domain.Enums.PersonContent;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.PersonContent.Handlers
{
    public class CopyDataHoldersCommandHandler : IRequestHandler<CopyDataHoldersCommand>
    {
        private readonly IApplicationDbContext _context;

        public CopyDataHoldersCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CopyDataHoldersCommand request, CancellationToken cancellationToken)
        {
            DataBlock dataBlock = await _context.DataBlocks
                .Include(db => db.DataHolders)
                .SingleOrDefaultAsync(db => db.CreatedBy.Equals(request.UserId) &&
                                            db.Id == request.DataBlockId,
                                      cancellationToken);

            if (dataBlock == null)
                throw new NotFoundException(nameof(DataBlock), request.DataBlockId);

            foreach (var item in request.DataHoldersIds)
            {
                DataHolder dataHolder = await _context.DataHolders
                    .SingleOrDefaultAsync(dh => dh.CreatedBy.Equals(request.UserId) &&
                                                dh.Id == item,
                                          cancellationToken);

                if (dataHolder == null)
                    continue;

                DataHolder entity = new DataHolder()
                {
                    Title = dataHolder.Title,
                    Data = dataHolder.Data,
                    DataHolderType = dataHolder.DataHolderType,
                    IsDeletable = true,
                    OrderNumber = dataBlock.DataHolders.Count + 1
                };

                var privacy = _context.DataHolderPrivacies
                    .SingleOrDefault(dhp => dhp.DataHolderId == dataHolder.Id);

                DataHolderPrivacy dataHolderPrivacy = new DataHolderPrivacy()
                {
                    DataHolder = entity,
                    BeginDate = privacy.BeginDate,
                    EndDate = privacy.EndDate,
                    IsAlways = privacy.IsAlways,
                    PrivacyLevel = privacy.PrivacyLevel
                };

                _context.DataHolderPrivacies.Add(dataHolderPrivacy);

                dataBlock.DataHolders.Add(entity);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
