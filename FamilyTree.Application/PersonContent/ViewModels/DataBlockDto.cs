using System.Collections.Generic;

namespace FamilyTree.Application.PersonContent.ViewModels
{
    public class DataBlockDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<DataHolderDto> DataHolders { get; set; }
    }
}
