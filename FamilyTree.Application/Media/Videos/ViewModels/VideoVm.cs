﻿using System.IO;

namespace FamilyTree.Application.Media.Videos.ViewModels
{
    public class VideoVm
    {
        public FileStream FileStream { get; set; }

        public string FileFormat { get; set; }
    }
}