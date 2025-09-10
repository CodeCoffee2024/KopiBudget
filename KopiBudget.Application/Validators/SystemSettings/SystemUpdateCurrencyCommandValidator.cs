using FluentValidation;
using KopiBudget.Application.Commands.System.SystemUpdateCurrency;

namespace KopiBudget.Application.Validators.SystemSettings
{
    public class SystemUpdateCurrencyCommandValidator : AbstractValidator<SystemUpdateCurrencyCommand>
    {
        #region Public Constructors

        public SystemUpdateCurrencyCommandValidator()
        {
            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required")
                .MaximumLength(3);
        }

        #endregion Public Constructors
    }
}