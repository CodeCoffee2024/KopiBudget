using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace KopiBudget.Application.Queries.Transaction.GetTransactions
{
    public class GetTransactionsQueryHandler(
        ITransactionRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetTransactionsQuery, Result<PageResult<TransactionDto>>>
    {
        #region Public Methods
        public async Task<Result<PageResult<TransactionDto>>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            DateTime? dateFrom = null;
            DateTime? dateTo = null;

            if (!string.IsNullOrWhiteSpace(request.DateFrom) && DateTime.TryParse(request.DateFrom, out var df))
                dateFrom = DateTime.SpecifyKind(df, DateTimeKind.Utc);

            if (!string.IsNullOrWhiteSpace(request.DateTo) && DateTime.TryParse(request.DateTo, out var dt))
                dateTo = DateTime.SpecifyKind(dt, DateTimeKind.Utc);

            if (string.IsNullOrWhiteSpace(request.DateFrom))
                dateFrom = DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);

            if (string.IsNullOrWhiteSpace(request.DateTo))
                dateTo = DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc);

            var categoryGuids = new List<Guid>();
            if (request.CategoryIds != null && request.CategoryIds.Count() > 0)
            {
                foreach (string categoryId in request.CategoryIds)
                {
                    if (!String.IsNullOrEmpty(categoryId))
                    {
                        if (!Guid.TryParse(categoryId, out var guid))
                        {
                            return Result.Failure<PageResult<TransactionDto>>(Error.InvalidRequest);
                        }
                        categoryGuids.Add(guid);
                    }
                }
            }

            var accountGuids = new List<Guid>();
            if (request.AccountIds != null && request.AccountIds.Count() > 0)
            {
                foreach (string accountId in request.AccountIds)
                {
                    if (!String.IsNullOrEmpty(accountId))
                    {
                        if (!Guid.TryParse(accountId, out var guid))
                        {
                            return Result.Failure<PageResult<TransactionDto>>(Error.InvalidRequest);
                        }
                        accountGuids.Add(guid);
                    }
                }
            }

            Expression<Func<Domain.Entities.Transaction, bool>> filter = c =>
                (dateFrom == null || c.Date >= dateFrom) &&
                (dateTo == null || c.Date <= dateTo) &&
                (c.Account.Name.Contains(request.Search!)) &&
                (categoryGuids.Count == 0 || categoryGuids.Contains(c.CategoryId)) &&
                (accountGuids.Count == 0 || accountGuids.Contains(c.AccountId));

            var pagedResult = await _repository.GetPaginatedCategoriesAsync(
                request.PageNumber,
                request.PageSize,
                request.Search,
                request.OrderBy!,
                filter);

            return Result.Success(
                new PageResult<TransactionDto>(
                    _mapper.Map<IReadOnlyList<TransactionDto>>(pagedResult.Items),
                    pagedResult.TotalCount,
                    pagedResult.PageNumber,
                    pagedResult.PageSize,
                    pagedResult.OrderBy)
            );
        }

        #endregion Public Methods
    }
}