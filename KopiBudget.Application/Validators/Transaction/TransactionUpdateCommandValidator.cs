using FluentValidation;
using KopiBudget.Application.Commands.Transaction.TransactionUpdate;
using KopiBudget.Domain.Entities;

namespace KopiBudget.Application.Validators.Transaction
{
    public class TransactionUpdateCommandValidator : AbstractValidator<TransactionUpdateCommand>
    {
        #region Public Constructors

        public TransactionUpdateCommandValidator()
        {
            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("Account is required")
                .When(x => x.Type == TransactionTypes.Account);

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount is required");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category is required")
                .When(x => x.Type == TransactionTypes.Account);

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required");

            RuleFor(x => x.Time)
                .NotEmpty().WithMessage("Time is required")
                .When(x => x.InputTime == true);

            RuleFor(x => x.PersonalCategoryId)
                .NotEmpty().WithMessage("Personal Category is required")
                .When(x => x.Type == TransactionTypes.Budget);

            RuleFor(x => x.Type)
                .Must(type => type == TransactionTypes.Account || type == TransactionTypes.Budget)
                .WithMessage($"Type can only be account or budget");

            RuleFor(x => x.BudgetId)
                .NotEmpty().WithMessage("Budget is required")
                .When(x => x.Type == TransactionTypes.Budget);
        }

        #endregion Public Constructors
    }
}