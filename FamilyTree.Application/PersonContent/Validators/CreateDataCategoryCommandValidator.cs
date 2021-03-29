using FamilyTree.Application.PersonContent.Commands;
using FluentValidation;

namespace FamilyTree.Application.PersonContent.Validators
{
    public class CreateDataCategoryCommandValidator : AbstractValidator<CreateDataCategoryCommand>
    {
        public CreateDataCategoryCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .Length(1, 50);
        }
    }
}
