using FluentValidation;
using KopiBudget.Application.Commands.PersonalCategory.PersonalCategoryCreate;

namespace KopiBudget.Application.Validators.PersonalCategory
{
    public class PersonalCategoryCreateCommandValidator : AbstractValidator<PersonalCategoryCreateCommand>
    {
        #region Public Constructors

        public PersonalCategoryCreateCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100);
            RuleFor(x => x.Icon)
                .NotEmpty().WithMessage("Icon is required")
                .MaximumLength(100);
            RuleFor(x => x.Color)
                .NotEmpty().WithMessage("Color is required")
                .MaximumLength(100);
        }

        #endregion Public Constructors
    }
}