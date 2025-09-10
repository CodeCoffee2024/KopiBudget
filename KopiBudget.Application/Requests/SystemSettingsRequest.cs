using KopiBudget.Application.Abstractions.Request;
using KopiBudget.Application.Commands.System.SystemUpdateCurrency;
using KopiBudget.Application.Queries.System.GetCurrencyDropdown;

namespace KopiBudget.Application.Requests
{
    public class SystemSettingsRequest : PageRequest
    {
        #region Properties

        public string? Currency { get; set; } = default!;

        #endregion Properties

        #region Public Methods

        public SystemUpdateCurrencyCommand SetUpdateCurrencyCommand(Guid UserId) =>
            new(UserId, Currency!);

        public GetCurrencyDropdownQuery ToDropdownQuery() => new(Search!, PageNumber);

        #endregion Public Methods
    }
}