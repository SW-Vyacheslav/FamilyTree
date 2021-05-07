﻿using FamilyTree.Application.Common.Exceptions;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.Media.Images.Commands;
using FamilyTree.Domain.Entities.Media;
using FamilyTree.Domain.Entities.PersonContent;
using FamilyTree.Domain.Entities.Privacy;
using FamilyTree.Domain.Enums.Privacy;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.Media.Images.Handlers
{
    public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateImageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            DataBlock dataBlock = await _context.DataBlocks
                .SingleOrDefaultAsync(db => db.CreatedBy.Equals(request.UserId) &&
                                            db.Id == request.DataBlockId,
                                      cancellationToken);

            if (dataBlock == null)
                throw new NotFoundException(nameof(DataBlock), request.DataBlockId);            

            Image entity = new Image();
            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.ImageType = request.ImageFile.ContentType.Split('/')[1];

            using (var streamReader = new BinaryReader(request.ImageFile.OpenReadStream()))
                entity.ImageData = streamReader.ReadBytes(Convert.ToInt32(request.ImageFile.Length));

            DataBlockImage dataBlockImage = new DataBlockImage();
            dataBlockImage.DataBlock = dataBlock;
            dataBlockImage.Image = entity;

            ImagePrivacy privacy = new ImagePrivacy()
            {
                Image = entity,
                PrivacyLevel = PrivacyLevel.Confidential,                
                IsAlways = true
            };

            _context.Images.Add(entity);
            _context.ImagePrivacies.Add(privacy);
            _context.DataBlockImages.Add(dataBlockImage);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
