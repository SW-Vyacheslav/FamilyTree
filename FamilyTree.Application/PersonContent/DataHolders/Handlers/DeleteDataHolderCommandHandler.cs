using FamilyTree.Application.Common.Exceptions;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.PersonContent.DataHolders.Commands;
using FamilyTree.Domain.Entities.PersonContent;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.PersonContent.DataHolders.Handlers
{
    public class DeleteDataHolderCommandHandler : IRequestHandler<DeleteDataHolderCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteDataHolderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteDataHolderCommand request, CancellationToken cancellationToken)
        {
            DataHolder dataHolder = await _context.DataHolders
                .SingleOrDefaultAsync(dh => dh.CreatedBy.Equals(request.UserId) &&
                                            dh.Id == request.Id,
                                      cancellationToken);

            if (dataHolder == null)
                throw new NotFoundException(nameof(DataHolder), request.Id);

            if (!dataHolder.IsDeletable.Value)
                throw new Exception("Can\'t delete DataHolder. This DataHolder isn\'t deletable");

            _context.DataHolders.Remove(dataHolder);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
