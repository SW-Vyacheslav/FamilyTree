using FamilyTree.Application.Common.Exceptions;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.PersonContent.Commands;
using FamilyTree.Application.PersonContent.Extensions;
using FamilyTree.Domain.Entities.PersonContent;
using FamilyTree.Domain.Entities.Privacy;
using FamilyTree.Domain.Enums.PersonContent;
using FamilyTree.Domain.Enums.Privacy;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.PersonContent.Handlers
{
    public class CreateDataHolderCommandHandler : IRequestHandler<CreateDataHolderCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateDataHolderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateDataHolderCommand request, CancellationToken cancellationToken)
        {
            DataBlock dataBlock = await _context.DataBlocks
                .Include(db => db.DataHolders)
                .SingleOrDefaultAsync(db => db.CreatedBy.Equals(request.UserId) &&
                                            db.Id == request.DataBlockId,
                                      cancellationToken);

            if (dataBlock == null)
                throw new NotFoundException(nameof(DataBlock), request.DataBlockId);

            if (!request.CanCreate())
                throw new ArgumentException(
                    $"DataHolder with DataHolderType = \"{request.DataHolderType}\" is not allowed to create.", 
                    nameof(request.DataHolderType));

            DataHolder entity = new DataHolder();
            entity.Data = request.Data;
            entity.Title = request.Title;
            entity.DataHolderType = request.DataHolderType;
            entity.DataBlockId = dataBlock.Id;
            entity.OrderNumber = dataBlock.DataHolders.Count() + 1;

            DataHolderPrivacy privacy = new DataHolderPrivacy();
            privacy.IsAlways = true;
            privacy.PrivacyLevel = PrivacyLevel.Confidential;
            privacy.DataHolder = entity;

            _context.DataHolders.Add(entity);
            _context.DataHolderPrivacies.Add(privacy);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
