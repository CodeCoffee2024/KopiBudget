using KopiBudget.Application.Abstractions.Request;
using KopiBudget.Application.Commands.Transaction.TransactionCreate;

namespace KopiBudget.Application.Requests
{
    public class TransactionRequest : PageRequest
    {
        #region Properties

        public Guid Id { get; set; }
        public decimal? Amount { get; set; } = decimal.Zero;
        public Guid? CategoryId { get; set; } = Guid.Empty;
        public Guid? AccountId { get; set; } = Guid.Empty;
        public DateTime Date { get; set; }
        public string? Time { get; set; } = string.Empty;
        public bool? InputTime { get; set; }
        public string? Note { get; set; } = string.Empty;

        #endregion Properties

        #region Public Methods

        public TransactionCreateCommand SetAddCommand(Guid UserId) =>
            new(UserId, CategoryId!.Value, AccountId!.Value, Date, Time, Amount!.Value, Note!, InputTime);

        //public GetCategoryListQuery ToQuery() => new(Search, OrderBy, PageNumber, PageSize, Status);

        //public AccountDropdownQuery ToDropdownQuery() => new(Search!, PageNumber);

        #endregion Public Methods
    }
}