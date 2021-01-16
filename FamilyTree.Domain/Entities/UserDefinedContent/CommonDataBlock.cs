namespace FamilyTree.Domain.Entities.UserDefinedContent
{
    public class CommonDataBlock
    {
        public int Id { get; set; }

        public DataBlock DataBlock { get; set; }

        public Category Category { get; set; }
    }
}
