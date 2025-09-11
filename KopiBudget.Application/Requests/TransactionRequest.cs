using KopiBudget.Application.Abstractions.Request;
using KopiBudget.Application.Commands.Transaction.TransactionCreate;
using KopiBudget.Application.Queries.Transaction.GetTransactions;

namespace KopiBudget.Application.Requests
{
    public class TransactionRequest : PageRequest
    {
        #region Properties

        public IEnumerable<string> AccountIds { get; set; } = new List<string>();
        public IEnumerable<string> CategoryIds { get; set; } = new List<string>();
        public Guid Id { get; set; }
        public decimal? Amount { get; set; } = decimal.Zero;
        public Guid? CategoryId { get; set; } = Guid.Empty;
        public Guid? AccountId { get; set; } = Guid.Empty;
        public DateTime Date { get; set; }
        public string DateFrom { get; set; } = string.Empty;
        public string DateTo { get; set; } = string.Empty;
        public string? Time { get; set; } = string.Empty;
        public bool? InputTime { get; set; }
        public string? Note { get; set; } = string.Empty;

        #endregion Properties

        #region Public Methods

        public TransactionCreateCommand SetAddCommand(Guid UserId) =>
            new(UserId, CategoryId!.Value, AccountId!.Value, Date, Time, Amount!.Value, Note!, InputTime);

        public GetTransactionsQuery ToQuery() => new(Search, OrderBy, PageNumber, PageSize, AccountIds, CategoryIds, DateFrom, DateTo);

        //public AccountDropdownQuery ToDropdownQuery() => new(Search!, PageNumber);

        #endregion Public Methods
    }
}