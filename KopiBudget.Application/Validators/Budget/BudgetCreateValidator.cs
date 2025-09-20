using FluentValidation;
using KopiBudget.Application.Commands.Budget.BudgetCreate;

namespace KopiBudget.Application.Validators.Budget
{
    public class BudgetCreateValidator : AbstractValidator<BudgetCreateCommand>
    {
        #region Public Constructors

        public BudgetCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100);
            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount is required")
                .MaximumLength(100);
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start Date is required")
                .MaximumLength(100);
            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End Date is required")
                .MaximumLength(100);
            RuleFor(x => x.BudgetPersonalCategories)
                .NotEmpty().WithMessage("At least 1 personal categories is required");
            RuleForEach(x => x.BudgetPersonalCategories).ChildRules(cat =>
            {
                cat.RuleFor(c => Decimal.Parse(c.Limit!))
                    .GreaterThan(0).WithMessage("Limit must be greater than 0");

                cat.RuleFor(c => c.PersonalCategoryId)
                    .NotNull().WithMessage("Personal category is required");
            });
        }

        #endregion Public Constructors
    }
}