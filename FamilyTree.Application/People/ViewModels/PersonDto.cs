using FamilyTree.Application.Media.Images.ViewModels;

namespace FamilyTree.Application.People.ViewModels
{
    public class PersonDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Middlename { get; set; }

        public ImageDto AvatarImage { get; set; }
    }
}
