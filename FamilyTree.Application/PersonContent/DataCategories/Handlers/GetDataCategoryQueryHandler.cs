﻿using FamilyTree.Application.Common.Exceptions;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.PersonContent.DataBlocks.ViewModels;
using FamilyTree.Application.PersonContent.DataCategories.Queries;
using FamilyTree.Application.PersonContent.DataCategories.ViewModels;
using FamilyTree.Application.PersonContent.DataHolders.ViewModels;
using FamilyTree.Application.Privacy.ViewModels;
using FamilyTree.Domain.Entities.PersonContent;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.PersonContent.DataCategories.Handlers
{
    public class GetDataCategoryQueryHandler : IRequestHandler<GetDataCategoryQuery, DataCategoryVm>
    {
        private const string DataHolderPrivacyFiller = "#####################";

        private readonly IApplicationDbContext _context;

        private readonly IDateTimeService _dateTimeService;

        public GetDataCategoryQueryHandler(IApplicationDbContext context, IDateTimeService dateTimeService)
        {
            _context = context;
            _dateTimeService = dateTimeService;
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
                DataCategoryType = dataCategory.DataCategoryType,
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
                    var privacy = await _context.DataHolderPrivacies
                        .SingleOrDefaultAsync(p => p.DataHolderId == dataHolder.Id,
                                              cancellationToken);

                    DataHolderDto dataHolderDto = new DataHolderDto()
                    {
                        Id = dataHolder.Id,
                        Title = dataHolder.Title,
                        DataHolderType = dataHolder.DataHolderType,
                        Data = dataHolder.Data,
                        IsDeletable = dataHolder.IsDeletable.Value                        
                    };

                    if (privacy != null)
                    {
                        dataHolderDto.Privacy = new DataHolderPrivacyDto()
                        {
                            Id = privacy.Id,
                            BeginDate = privacy.BeginDate,
                            EndDate = privacy.EndDate,
                            IsAlways = privacy.IsAlways.Value,
                            PrivacyLevel = privacy.PrivacyLevel
                        };

                        if (!privacy.IsAlways.Value)
                        {
                            var nowTime = _dateTimeService.Now;

                            if (nowTime >= privacy.BeginDate &&
                                nowTime <= privacy.EndDate)
                            {
                                dataHolderDto.Data = DataHolderPrivacyFiller;
                            }
                        }
                    }

                    dataBlockDto.DataHolders.Add(dataHolderDto);
                }

                result.DataBlocks.Add(dataBlockDto);
            }

            return result;
        }
    }
}
