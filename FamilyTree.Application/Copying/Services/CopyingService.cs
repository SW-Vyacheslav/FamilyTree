using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.Copying.Interfaces;
using FamilyTree.Domain.Entities.Media;
using FamilyTree.Domain.Entities.PersonContent;
using FamilyTree.Domain.Entities.Privacy;
using FamilyTree.Domain.Entities.Tree;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.Copying.Services
{
    /// <summary>
    /// ICopyingService implementation.
    /// Copying data to IApplicationDbContext without saving.
    /// </summary>
    public class CopyingService : ICopyingService
    {
        private readonly IApplicationDbContext _context;

        public CopyingService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataCategory> CopyDataCategoryToPerson(Person person, DataCategory dataCategory, CancellationToken cancellationToken)
        {
            var dataCategoriesCount = await _context.DataCategories
                .CountAsync(dc => dc.PersonId == person.Id,
                            cancellationToken);

            DataCategory entity = new DataCategory()
            {
                Name = dataCategory.Name,
                DataCategoryType = dataCategory.DataCategoryType,
                OrderNumber = dataCategoriesCount + 1,
                Person = person
            };

            entity.DataBlocks = new List<DataBlock>();

            foreach (var dataBlock in dataCategory.DataBlocks)
            {
                entity.DataBlocks
                    .Add(await CopyDataBlockToDataCategory(entity, dataBlock, cancellationToken));
            }

            return entity;
        }

        public async Task<DataBlock> CopyDataBlockToDataCategory(DataCategory dataCategory, DataBlock dataBlock, CancellationToken cancellationToken)
        {
            DataBlock entity = new DataBlock()
            {
                DataCategory = dataCategory,
                Title = dataBlock.Title,
                OrderNumber = dataCategory.DataBlocks.Count + 1,
                DataHolders = new List<DataHolder>()
            };

            foreach (var dataHolder in dataBlock.DataHolders)
            {
                _context.DataHolders
                    .Add(await CopyDataHolderToDataBlock(entity, dataHolder, cancellationToken));
            }

            var images = await _context.DataBlockImages
                .Where(dbi => dbi.DataBlockId == dataBlock.Id)
                .Select(dbi => dbi.Image)
                .ToListAsync(cancellationToken);

            foreach (var image in images)
            {
                _context.Images
                    .Add(await CopyImageToDataBlock(entity, image, cancellationToken));
            }

            var videos = await _context.DataBlockVideos
                .Where(dbv => dbv.DataBlockId == dataBlock.Id)
                .Select(dbv => dbv.Video)
                .ToListAsync(cancellationToken);

            foreach (var video in videos)
            {
                _context.Videos
                    .Add(await CopyVideoToDataBlock(entity, video, cancellationToken));
            }

            return entity;
        }

        public async Task<DataHolder> CopyDataHolderToDataBlock(DataBlock dataBlock, DataHolder dataHolder, CancellationToken cancellationToken)
        {
            DataHolder entity = new DataHolder()
            {
                DataBlock = dataBlock,
                Title = dataHolder.Title,
                Data = dataHolder.Data,
                DataHolderType = dataHolder.DataHolderType,
                IsDeletable = true,
                OrderNumber = dataBlock.DataHolders.Count + 1
            };

            var privacy = await _context.DataHolderPrivacies
                .SingleOrDefaultAsync(dhp => dhp.DataHolderId == dataHolder.Id);

            DataHolderPrivacy dataHolderPrivacy = new DataHolderPrivacy()
            {
                DataHolder = entity,
                BeginDate = privacy.BeginDate,
                EndDate = privacy.EndDate,
                IsAlways = privacy.IsAlways,
                PrivacyLevel = privacy.PrivacyLevel
            };

            _context.DataHolderPrivacies.Add(dataHolderPrivacy);

            return entity;
        }

        public async Task<Image> CopyImageToDataBlock(DataBlock dataBlock, Image image, CancellationToken cancellationToken)
        {
            Image entity = new Image()
            {
                Title = image.Title,
                Description = image.Description,
                ImageData = image.ImageData,
                ImageFormat = image.ImageFormat
            };

            DataBlockImage dataBlockImage = new DataBlockImage()
            {
                DataBlock = dataBlock,
                Image = entity
            };

            var privacy = await _context.ImagePrivacies
                .SingleOrDefaultAsync(ip => ip.ImageId == image.Id,
                                      cancellationToken);

            ImagePrivacy imagePrivacy = new ImagePrivacy()
            {
                Image = entity,
                PrivacyLevel = privacy.PrivacyLevel,
                BeginDate = privacy.BeginDate,
                EndDate = privacy.EndDate,
                IsAlways = privacy.IsAlways
            };

            _context.DataBlockImages.Add(dataBlockImage);
            _context.ImagePrivacies.Add(imagePrivacy);

            return entity;
        }

        public async Task<Video> CopyVideoToDataBlock(DataBlock dataBlock, Video video, CancellationToken cancellationToken)
        {
            Video entity = new Video
            {
                Title = video.Title,
                Description = video.Description,
                PreviewImageData = video.PreviewImageData,
                PreviewImageFormat = video.PreviewImageFormat,
                FilePath = video.FilePath
            };

            DataBlockVideo dataBlockVideo = new DataBlockVideo
            {
                DataBlock = dataBlock,
                Video = entity
            };

            var privacy = await _context.VideoPrivacies
                .SingleOrDefaultAsync(vp => vp.VideoId == video.Id,
                                      cancellationToken);

            VideoPrivacy videoPrivacy = new VideoPrivacy()
            {
                Video = entity,
                PrivacyLevel = privacy.PrivacyLevel,
                BeginDate = privacy.BeginDate,
                EndDate = privacy.EndDate,
                IsAlways = privacy.IsAlways
            };

            _context.DataBlockVideos.Add(dataBlockVideo);
            _context.VideoPrivacies.Add(videoPrivacy);

            return entity;
        }
    }
}
