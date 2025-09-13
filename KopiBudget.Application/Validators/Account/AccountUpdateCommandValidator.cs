using FluentValidation;
using KopiBudget.Application.Commands.Account.AccountUpdate;

namespace KopiBudget.Application.Validators.Account
{
    public class AccountUpdateCommandValidator : AbstractValidator<AccountUpdateCommand>
    {
        #region Public Constructors

        public AccountUpdateCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Account is required")
                .MaximumLength(100);

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