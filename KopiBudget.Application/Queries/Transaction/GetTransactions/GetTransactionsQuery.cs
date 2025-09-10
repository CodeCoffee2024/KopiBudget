using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.Transaction.GetTransactions
{
    public class GetTransactionsQuery : PagedQuery, IRequest<Result<PageResult<TransactionDto>>>
    {
        #region Public Constructors

        public List<string> CategoryIds = new List<string>();

        public List<string> AccountIds = new List<string>();

        public GetTransactionsQuery(string? search = null, string? orderBy = "Name", int pageNumber = 1, int pageSize = 1, List<string> accountIds = null, List<string> categoryIds = null, string dateFrom = "", string dateTo = "")
        {
            Search = search;
            OrderBy = orderBy;
            PageNumber = pageNumber;
            PageSize = pageSize;
            AccountIds = accountIds;
            CategoryIds = categoryIds;
            AccountIds = accountIds;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public string? DateFrom { get; set; } = string.Empty;
        public string? DateTo { get; set; } = string.Empty;

        #endregion Public Constructors
    }
}