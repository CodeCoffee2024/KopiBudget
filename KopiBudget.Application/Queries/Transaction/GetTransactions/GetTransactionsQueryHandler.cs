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
            Expression<Func<Domain.Entities.Transaction, bool>> filter = c =>
            (request.DateFrom == null && request.DateTo == null) ||
            (c.Date >= (request.DateFrom != null ? DateTime.Parse(request.DateFrom) : DateTime.MinValue) &&
             c.Date <= (request.DateTo != null ? DateTime.Parse(request.DateTo) : DateTime.MaxValue));

            // Handle CategoryIds
            if (request.CategoryIds != null && request.CategoryIds.Count > 0)
            {
                foreach (string categoryId in request.CategoryIds)
                {
                    if (!Guid.TryParse(categoryId, out _))
                    {
                        return Result.Failure<PageResult<TransactionDto>>(Error.InvalidRequest);
                    }
                }

                var categoryGuids = request.CategoryIds
                    .Where(x => Guid.TryParse(x, out _))
                    .Select(Guid.Parse)
                    .ToList();

                filter = c => (
                        (request.DateFrom == null && request.DateTo == null) ||
                        (c.Date >= (request.DateFrom != null ? DateTime.Parse(request.DateFrom) : DateTime.MinValue) &&
                         c.Date <= (request.DateTo != null ? DateTime.Parse(request.DateTo) : DateTime.MaxValue))
                    )
                    && categoryGuids.Contains(c.CategoryId);
            }

            // Handle AccountIds
            if (request.AccountIds != null && request.AccountIds.Count > 0)
            {
                foreach (string accountId in request.AccountIds)
                {
                    if (!Guid.TryParse(accountId, out _))
                    {
                        return Result.Failure<PageResult<TransactionDto>>(Error.InvalidRequest);
                    }
                }

                var accountGuids = request.AccountIds
                    .Where(x => Guid.TryParse(x, out _))
                    .Select(Guid.Parse)
                    .ToList();

                filter = c => (
                        (request.DateFrom == null && request.DateTo == null) ||
                        (c.Date >= (request.DateFrom != null ? DateTime.Parse(request.DateFrom) : DateTime.MinValue) &&
                         c.Date <= (request.DateTo != null ? DateTime.Parse(request.DateTo) : DateTime.MaxValue))
                    )
                    && (request.CategoryIds == null || request.CategoryIds.Count == 0 || request.CategoryIds.Contains(c.CategoryId.ToString()))
                    && accountGuids.Contains(c.AccountId);
            }

            var pagedResult = await _repository.GetPaginatedCategoriesAsync(
                request.PageNumber, request.PageSize, request.Search, request.OrderBy!, filter);

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