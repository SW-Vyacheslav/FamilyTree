using System.Collections.Generic;

namespace FamilyTree.Domain.Entities.UserDefinedContent
{
    public class DataBlock
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int OrderNumber { get; set; }

        public Category Category { get; set; }

        public List<DataHolder> DataHolders { get; set; }
    }
}