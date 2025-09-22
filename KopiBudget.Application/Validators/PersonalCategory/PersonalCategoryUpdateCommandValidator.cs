using FluentValidation;
using KopiBudget.Application.Commands.PersonalCategory.PersonalCategoryUpdate;

namespace KopiBudget.Application.Validators.PersonalCategory
{
    public class PersonalCategoryUpdateCommandValidator : AbstractValidator<PersonalCategoryUpdateCommand>
    {
        #region Public Constructors

        public PersonalCategoryUpdateCommandValidator()
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