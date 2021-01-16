using FamilyTree.Domain.Enums.UserDefinedContent;

namespace FamilyTree.Domain.Entities.UserDefinedContent
{
    public class DataHolder
    {
        public int Id { get; set; }

        public DataHolderType DataHolderType { get; set; }

        public string Title { get; set; }

        public string Data { get; set; }

        public bool IsDeletable { get; set; }

        public int OrderNumber { get; set; }

        public DataBlock DataBlock { get; set; }
    }
}