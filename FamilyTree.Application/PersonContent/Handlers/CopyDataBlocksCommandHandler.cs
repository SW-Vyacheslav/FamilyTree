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
    public class CopyDataBlocksCommandHandler : IRequestHandler<CopyDataBlocksCommand>
    {
        private readonly IApplicationDbContext _context;

        public CopyDataBlocksCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CopyDataBlocksCommand request, CancellationToken cancellationToken)
        {
            DataCategory dataCategory = await _context.DataCategories
                .Include(dc => dc.DataBlocks)
                .SingleOrDefaultAsync(dc => dc.CreatedBy.Equals(request.UserId) &&
                                            dc.Id == request.DataCategoryId,
                                      cancellationToken);

            if (dataCategory == null)
                throw new NotFoundException(nameof(DataCategory), request.DataCategoryId);

            if (dataCategory.DataCategoryType == DataCategoryType.InfoBlock ||
                dataCategory.DataCategoryType == DataCategoryType.PersonInfo)
                throw new Exception($"Can not copy to DataCategory with CategoryType = \"{dataCategory.DataCategoryType}\"");

            foreach (var item in request.DataBlocksIds)
            {
                DataBlock dataBlock = await _context.DataBlocks
                    .Include(db => db.DataHolders)
                    .SingleOrDefaultAsync(db => db.CreatedBy.Equals(request.UserId) &&
                                                db.Id == item,
                                          cancellationToken);

                if (dataBlock == null)
                    continue;

                DataBlock entity = new DataBlock();
                entity.Title = dataBlock.Title;
                entity.OrderNumber = dataCategory.DataBlocks.Count + 1;
                entity.DataHolders = dataBlock.DataHolders
                    .Select(dh =>
                    {
                        DataHolder dataHolder = new DataHolder() 
                        {
                            Title = dh.Title,
                            Data = dh.Data,
                            DataHolderType = dh.DataHolderType,
                            IsDeletable = true,
                            OrderNumber = dh.OrderNumber
                        };

                        var privacy = _context.DataHolderPrivacies
                            .SingleOrDefault(dhp => dhp.DataHolderId == dh.Id);

                        DataHolderPrivacy dataHolderPrivacy = new DataHolderPrivacy()
                        {
                            DataHolder = dataHolder,
                            BeginDate = privacy.BeginDate,
                            EndDate = privacy.EndDate,
                            IsAlways = privacy.IsAlways,
                            PrivacyLevel = privacy.PrivacyLevel
                        };

                        _context.DataHolderPrivacies.Add(dataHolderPrivacy);

                        return dataHolder;
                    })
                    .ToList();

                dataCategory.DataBlocks.Add(entity);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
