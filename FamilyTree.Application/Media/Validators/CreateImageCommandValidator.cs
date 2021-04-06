using FamilyTree.Application.Media.Commands;
using FluentValidation;

namespace FamilyTree.Application.Media.Validators
{
    public class CreateImageCommandValidator : AbstractValidator<CreateImageCommand>
    {
        public CreateImageCommandValidator()
        {
            RuleFor(i => i.Title)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(i => i.Description)
                .MaximumLength(1000);

            RuleFor(i => i.ImageFile)
                .NotEmpty();
        }
    }
}
