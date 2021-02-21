using FamilyTree.Application.Common.Exceptions;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.PersonContent.Queries;
using FamilyTree.Application.PersonContent.ViewModels;
using FamilyTree.Domain.Entities.PersonContent;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.PersonContent.Handlers
{
    public class GetDataCategoryQueryHandler : IRequestHandler<GetDataCategoryQuery, DataCategoryVm>
    {
        private readonly IApplicationDbContext _context;

        public GetDataCategoryQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataCategoryVm> Handle(GetDataCategoryQuery request, CancellationToken cancellationToken)
        {
            DataCategory dataCategory = await _context.DataCategories
                .Include(dc => dc.DataBlocks)
                .ThenInclude(db => db.DataHolders)
                .SingleOrDefaultAsync(dc => dc.CreatedBy.Equals(request.UserId) &&
                                            dc.Id == request.DataCategoryId,
                                      cancellationToken);

            if (dataCategory == null)
                throw new NotFoundException(nameof(DataCategory), request.DataCategoryId);

            DataCategoryVm result = new DataCategoryVm()
            {
                Id = dataCategory.Id,
                Name = dataCategory.Name,
                CategoryType = dataCategory.CategoryType,
                IsDeletable = dataCategory.IsDeletable.Value
            };
            result.DataBlocks = new List<DataBlockDto>();

            List<DataBlock> dataBlocks = dataCategory.DataBlocks
                .OrderBy(db => db.OrderNumber)
                .ToList();

            foreach (DataBlock dataBlock in dataBlocks)
            {
                DataBlockDto dataBlockDto = new DataBlockDto()
                {
                    Id = dataBlock.Id,
                    Title = dataBlock.Title
                };
                dataBlockDto.DataHolders = new List<DataHolderDto>();

                List<DataHolder> dataHolders = dataBlock.DataHolders
                    .OrderBy(dh => dh.OrderNumber)
                    .ToList();

                foreach (DataHolder dataHolder in dataHolders)
                {
                    DataHolderDto dataHolderDto = new DataHolderDto()
                    {
                        Id = dataHolder.Id,
                        Title = dataHolder.Title,
                        DataHolderType = dataHolder.DataHolderType,
                        Data = dataHolder.Data,
                        IsDeletable = dataHolder.IsDeletable.Value
                    };

                    dataBlockDto.DataHolders.Add(dataHolderDto);
                }

                result.DataBlocks.Add(dataBlockDto);
            }

            return result;
        }
    }
}
