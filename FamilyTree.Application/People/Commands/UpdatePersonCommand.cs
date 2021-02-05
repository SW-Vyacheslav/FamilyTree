using MediatR;
using System;

namespace FamilyTree.Application.People.Commands
{
    public class UpdatePersonCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Middlename { get; set; }

        public DateTime? Birthday { get; set; }

        public string Gender { get; set; }

        public string UserId { get; set; }
    }
}
