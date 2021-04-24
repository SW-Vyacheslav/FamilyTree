﻿using FamilyTree.Domain.Entities.Media;
using FamilyTree.Domain.Entities.PersonContent;
using FamilyTree.Domain.Entities.Tree;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.Copying.Interfaces
{
    public interface ICopyingService
    {
        Task<DataCategory> CopyDataCategoryToPerson(Person person, DataCategory dataCategory, CancellationToken cancellationToken);

        Task<DataBlock> CopyDataBlockToDataCategory(DataCategory dataCategory, DataBlock dataBlock, CancellationToken cancellationToken);

        Task<DataHolder> CopyDataHolderToDataBlock(DataBlock dataBlock, DataHolder dataHolder, CancellationToken cancellationToken);

        Task<Image> CopyImageToDataBlock(DataBlock dataBlock, Image image, CancellationToken cancellationToken);

        Task<Video> CopyVideoToDataBlock(DataBlock dataBlock, Video video, CancellationToken cancellationToken);
    }
}
