using FluentValidation;
using KopiBudget.Application.Commands.Category.CategoryCreate;

namespace KopiBudget.Application.Validators.Category
{
    public class CategoryCreateCommandValidator : AbstractValidator<CategoryCreateCommand>
    {
        #region Public Constructors

        public CategoryCreateCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100);
        }

        #endregion Public Constructors
    }
}