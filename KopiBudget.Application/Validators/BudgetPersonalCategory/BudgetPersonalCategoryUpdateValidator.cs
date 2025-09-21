using FluentValidation;
using KopiBudget.Application.Commands.BudgetPersonalCategoryCmd.BudgetPersonalCategoryUpdate;

namespace KopiBudget.Application.Validators.BudgetPersonalCategory
{
    public class BudgetPersonalCategoryUpdateValidator : AbstractValidator<BudgetPersonalCategoryUpdateCommand>
    {
        #region Public Constructors

        public BudgetPersonalCategoryUpdateValidator()
        {
            RuleFor(x => x.Limit)
                .NotEmpty().WithMessage("Limit is required");
        }

        #endregion Public Constructors
    }
}