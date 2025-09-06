using FluentValidation;
using KopiBudget.Application.Commands.Account.AccountCreate;

namespace KopiBudget.Application.Validators.Account
{
    public class AccountCreateCommandValidator : AbstractValidator<AccountCreateCommand>
    {
        #region Public Constructors

        public AccountCreateCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100);

            RuleFor(x => x.Balance)
                .NotEmpty().WithMessage("Balance is required")
                .GreaterThan(0).WithMessage("Balance must be greater than 0.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category is required");
        }

        #endregion Public Constructors
    }
}