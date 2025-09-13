using KopiBudget.Application.Abstractions.Request;
using KopiBudget.Application.Commands.Account.AccountCreate;
using KopiBudget.Application.Commands.Account.AccountUpdate;
using KopiBudget.Application.Queries.Account.AccountDropdown;

namespace KopiBudget.Application.Requests
{
    public class AccountRequest : PageRequest
    {
        #region Properties

        public Guid Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public decimal? Balance { get; set; } = decimal.Zero;
        public bool IsDebt { get; set; }
        public string? CategoryId { get; set; } = string.Empty;

        #endregion Properties

        #region Public Methods

        public AccountCreateCommand SetAddCommand(Guid UserId) =>
            new(UserId, CategoryId, Name!, Balance!.Value, IsDebt);

        public AccountUpdateCommand SetUpdateCommand(Guid UserId, string Id) =>
            new(UserId, Id, CategoryId, Name!, Balance!.Value, IsDebt);

        public AccountDropdownQuery ToDropdownQuery() => new(Search!, PageNumber, Exclude);

        #endregion Public Methods
    }
}