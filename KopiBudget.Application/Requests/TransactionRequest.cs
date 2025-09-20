using KopiBudget.Application.Abstractions.Request;
using KopiBudget.Application.Commands.Transaction.TransactionCreate;
using KopiBudget.Application.Commands.Transaction.TransactionUpdate;
using KopiBudget.Application.Queries.Transaction.GetTransactions;

namespace KopiBudget.Application.Requests
{
    public class TransactionRequest : PageRequest
    {
        #region Properties

        public IEnumerable<string> AccountIds { get; set; } = new List<string>();
        public IEnumerable<string> CategoryIds { get; set; } = new List<string>();
        public IEnumerable<string> BudgetIds { get; set; } = new List<string>();
        public IEnumerable<string> PersonalCategoryIds { get; set; } = new List<string>();
        public Guid Id { get; set; }
        public string? Amount { get; set; } = string.Empty;
        public string? CategoryId { get; set; } = string.Empty;
        public string? Type { get; set; } = string.Empty;
        public string? AccountId { get; set; } = string.Empty;
        public string? PersonalCategoryId { get; set; } = string.Empty;
        public string? BudgetId { get; set; } = string.Empty;
        public string? Date { get; set; } = string.Empty;
        public string DateFrom { get; set; } = string.Empty;
        public string DateTo { get; set; } = string.Empty;
        public string? Time { get; set; } = string.Empty;
        public bool? InputTime { get; set; }
        public string? Note { get; set; } = string.Empty;

        #endregion Properties

        #region Public Methods

        public TransactionCreateCommand SetAddCommand(Guid UserId) =>
            new(UserId, CategoryId, AccountId, PersonalCategoryId, BudgetId, Type, Date, Time, Amount, Note!, InputTime);

        public TransactionUpdateCommand SetUpdateCommand(Guid UserId, string Id) =>
            new(UserId, Id, CategoryId, AccountId, PersonalCategoryId, BudgetId, Type, Date, Time, Amount, Note!, InputTime);

        public GetTransactionsQuery ToQuery() => new(Search, OrderBy, PageNumber, PageSize, AccountIds, CategoryIds, BudgetIds, PersonalCategoryIds, Type, DateFrom, DateTo);

        //public AccountDropdownQuery ToDropdownQuery() => new(Search!, PageNumber);

        #endregion Public Methods
    }
}