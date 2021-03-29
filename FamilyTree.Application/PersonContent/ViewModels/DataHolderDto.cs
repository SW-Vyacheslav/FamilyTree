using FamilyTree.Application.Privacy.ViewModels;
using FamilyTree.Domain.Enums.PersonContent;

namespace FamilyTree.Application.PersonContent.ViewModels
{
    public class DataHolderDto
    {
        public int Id { get; set; }

        public DataHolderType DataHolderType { get; set; }

        public string Title { get; set; }

        public string Data { get; set; }

        public bool IsDeletable { get; set; }

        public DataHolderPrivacyDto Privacy { get; set; }
    }
}
