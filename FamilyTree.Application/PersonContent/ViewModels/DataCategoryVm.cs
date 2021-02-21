using FamilyTree.Domain.Enums.PersonContent;
using System.Collections.Generic;

namespace FamilyTree.Application.PersonContent.ViewModels
{
    public class DataCategoryVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CategoryType CategoryType { get; set; }

        public bool IsDeletable { get; set; }

        public List<DataBlockDto> DataBlocks { get; set; }
    }
}
