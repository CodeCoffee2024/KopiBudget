using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.Transaction.GetTransactions
{
    public class GetTransactionsQuery : PagedQuery, IRequest<Result<PageResult<TransactionDto>>>
    {
        #region Public Constructors

        public GetTransactionsQuery(
            string? search = null,
            string? orderBy = "Name",
            int pageNumber = 1,
            int pageSize = 1,
            IEnumerable<string>? accountIds = null,
            IEnumerable<string>? categoryIds = null,
            string? dateFrom = null,
            string? dateTo = null)
        {
            Search = search;
            OrderBy = orderBy;
            PageNumber = pageNumber;
            PageSize = pageSize;
            AccountIds = accountIds ?? Enumerable.Empty<string>();
            CategoryIds = categoryIds ?? Enumerable.Empty<string>();
            DateFrom = string.IsNullOrWhiteSpace(dateFrom) ? null : dateFrom;
            DateTo = string.IsNullOrWhiteSpace(dateTo) ? null : dateTo;
        }

        public IEnumerable<string> CategoryIds { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<string> AccountIds { get; set; } = Enumerable.Empty<string>();
        public string? DateFrom { get; set; } = string.Empty;
        public string? DateTo { get; set; } = string.Empty;

        #endregion Public Constructors
    }
}