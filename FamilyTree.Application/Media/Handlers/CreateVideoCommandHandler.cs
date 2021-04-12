using FamilyTree.Application.Common.Exceptions;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.Media.Commands;
using FamilyTree.Domain.Entities.Media;
using FamilyTree.Domain.Entities.PersonContent;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.Media.Handlers
{
    //TODO:
    public class CreateVideoCommandHandler : IRequestHandler<CreateVideoCommand, int>
    {
        private readonly IApplicationDbContext _context;

        private readonly IHostingEnvironment _hostingEnvironment;

        public CreateVideoCommandHandler(IApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<int> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {
            DataBlock dataBlock = await _context.DataBlocks
                .SingleOrDefaultAsync(db => db.CreatedBy.Equals(request.UserId) &&
                                            db.Id == request.DataBlockId,
                                      cancellationToken);

            if (dataBlock == null)
                throw new NotFoundException(nameof(DataBlock), request.DataBlockId);

            Video entity = new Video();
            entity.Title = request.Title;
            entity.Description = request.Description;            

            if (request.IsUploadOnServer)
            {
                string fileName = $"{Guid.NewGuid()}.{request.VideoFile.ContentType.Split('/')[1]}";
                string path = $"{_hostingEnvironment.WebRootPath}/uploads/videos/{fileName}";

                using (var stream = File.OpenWrite(path))
                {
                    await request.VideoFile.CopyToAsync(stream);
                }

                entity.FilePath = path;
            }
            else
            {
                entity.FilePath = request.FilePath;
            }

            DataBlockVideo dataBlockVideo = new DataBlockVideo();
            dataBlockVideo.DataBlock = dataBlock;
            dataBlockVideo.Video = entity;

            _context.Videos.Add(entity);
            _context.DataBlockVideos.Add(dataBlockVideo);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
