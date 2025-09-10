using KopiBudget.Application.Abstractions.Request;
using KopiBudget.Application.Commands.Account.AccountCreate;
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
        public Guid CategoryId { get; set; }

        #endregion Properties

        #region Public Methods

        public AccountCreateCommand SetAddCommand(Guid UserId) =>
            new(UserId, CategoryId, Name!, Balance!.Value, IsDebt);

        //public GetCategoryListQuery ToQuery() => new(Search, OrderBy, PageNumber, PageSize, Status);

        public AccountDropdownQuery ToDropdownQuery() => new(Search!, PageNumber, Exclude);

        #endregion Public Methods
    }
}