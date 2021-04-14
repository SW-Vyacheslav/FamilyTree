using FamilyTree.Application.Common.Exceptions;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.PersonContent.Commands;
using FamilyTree.Domain.Entities.PersonContent;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.PersonContent.Handlers
{
    class DeleteDataCategoryCommandHandler : IRequestHandler<DeleteDataCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteDataCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteDataCategoryCommand request, CancellationToken cancellationToken)
        {
            DataCategory dataCategory = await _context.DataCategories
                .SingleOrDefaultAsync(dh => dh.CreatedBy.Equals(request.UserId) &&
                                            dh.Id == request.Id,
                                      cancellationToken);

            if (dataCategory == null)
                throw new NotFoundException(nameof(DataHolder), request.Id);

            if (!dataCategory.IsDeletable.Value)
                throw new Exception("Can\'t delete DataHolder. This DataHolder isn\'t deletable");

            _context.DataCategories.Remove(dataCategory);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
