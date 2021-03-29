using FamilyTree.Application.PersonContent.Commands;
using FluentValidation;

namespace FamilyTree.Application.PersonContent.Validators
{
    public class CreateDataBlockCommandValidator : AbstractValidator<CreateDataBlockCommand>
    {
        public CreateDataBlockCommandValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .Length(1, 50);
        }
    }
}
