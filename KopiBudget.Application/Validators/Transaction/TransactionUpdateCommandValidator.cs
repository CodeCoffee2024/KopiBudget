using FluentValidation;
using KopiBudget.Application.Commands.Transaction.TransactionUpdate;

namespace KopiBudget.Application.Validators.Transaction
{
    public class TransactionUpdateCommandValidator : AbstractValidator<TransactionUpdateCommand>
    {
        #region Public Constructors

        public TransactionUpdateCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");

            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("Account is required");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount is required");

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