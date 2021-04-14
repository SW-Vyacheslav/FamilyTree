﻿using FamilyTree.Domain.Common;

namespace FamilyTree.Domain.Entities.Media
{
    public class Video : AuditableEntity
    {
        public int Id { get; set; }

        public string FilePath { get; set; }

        public byte[] PreviewImageData { get; set; }

        public string PreviewImageFormat { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}