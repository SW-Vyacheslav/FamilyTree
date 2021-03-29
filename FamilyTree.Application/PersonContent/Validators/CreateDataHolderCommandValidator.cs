using FamilyTree.Application.PersonContent.Commands;
using FluentValidation;

namespace FamilyTree.Application.PersonContent.Validators
{
    public class CreateDataHolderCommandValidator : AbstractValidator<CreateDataHolderCommand>
    {
        public CreateDataHolderCommandValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .Length(1, 50);

            RuleFor(c => c.Data)
                .MaximumLength(5000);
        }
    }
}
