using FamilyTree.Application.Media.Videos.Commands;
using FluentValidation;

namespace FamilyTree.Application.Media.Videos.Validators
{
    public class CreateVideoCommandValidator : AbstractValidator<CreateVideoCommand>
    {
        public CreateVideoCommandValidator()
        {
            RuleFor(v => v.Title)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(v => v.Description)
                .MaximumLength(1000);

            RuleFor(v => v.VideoFile)
                .NotEmpty();
        }
    }
}
