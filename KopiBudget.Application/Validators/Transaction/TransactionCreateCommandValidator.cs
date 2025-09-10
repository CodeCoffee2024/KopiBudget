using FluentValidation;
using KopiBudget.Application.Commands.Transaction.TransactionCreate;

namespace KopiBudget.Application.Validators.Transaction
{
    public class TransactionCreateCommandValidator : AbstractValidator<TransactionCreateCommand>
    {
        #region Public Constructors

        public TransactionCreateCommandValidator()
        {
            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("Account is required");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount is required")
                .GreaterThan(0).WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category is required");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required");

            RuleFor(x => x.Time)
                .NotEmpty().WithMessage("Time is required")
                .When(x => x.InputTime == true);
        }

        #endregion Public Constructors
    }
}