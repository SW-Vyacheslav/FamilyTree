﻿using FamilyTree.Application.Common.Exceptions;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.Media.Queries;
using FamilyTree.Application.Media.ViewModels;
using FamilyTree.Domain.Entities.PersonContent;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.Media.Handlers
{
    public class GetVideosQueryHandler : IRequestHandler<GetVideosQuery, List<VideoDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetVideosQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<VideoDto>> Handle(GetVideosQuery request, CancellationToken cancellationToken)
        {
            DataBlock dataBlock = await _context.DataBlocks
                .SingleOrDefaultAsync(db => db.CreatedBy.Equals(request.UserId) &&
                                            db.Id == request.DataBlockId,
                                      cancellationToken);

            if (dataBlock == null)
                throw new NotFoundException(nameof(DataBlock), request.DataBlockId);

            var videos = await _context.DataBlockVideos
                .Include(dbv => dbv.Video)
                .Where(dbv => dbv.DataBlockId == dataBlock.Id)
                .Select(dbv => new VideoDto()
                {
                    Id = dbv.VideoId,
                    Title = dbv.Video.Title,
                    Description = dbv.Video.Description,
                    PreviewImageData = Convert.ToBase64String(dbv.Video.PreviewImageData),
                    PreviewImageFormat = dbv.Video.PreviewImageFormat
                })
                .ToListAsync(cancellationToken);

            return videos;
        }
    }
}
