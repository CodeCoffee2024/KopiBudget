using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Common.Utils;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
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
            // Normalize dates
            var dateFrom = !string.IsNullOrWhiteSpace(request.DateFrom) && DateTime.TryParse(request.DateFrom, out var df)
                ? DateTime.SpecifyKind(df, DateTimeKind.Utc)
                : DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);

            var dateTo = !string.IsNullOrWhiteSpace(request.DateTo) && DateTime.TryParse(request.DateTo, out var dt)
                ? DateTime.SpecifyKind(dt, DateTimeKind.Utc)
                : DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc);

            Expression<Func<Domain.Entities.Transaction, bool>> filter = c => true; // default

            if (request.Type == TransactionTypes.Account)
            {
                var categoryGuids = GuidParser.ParseGuids(request.CategoryIds);
                if (categoryGuids == null) return Result.Failure<PageResult<TransactionDto>>(Error.InvalidRequest);

                var accountGuids = GuidParser.ParseGuids(request.AccountIds);
                if (accountGuids == null) return Result.Failure<PageResult<TransactionDto>>(Error.InvalidRequest);

                filter = c =>
                    c.BudgetId == null &&
                    c.Date >= dateFrom &&
                    c.CreatedById == request.UserId &&
                    c.Date <= dateTo &&
                    (string.IsNullOrEmpty(request.Search) || c.Account.Name.Contains(request.Search)) &&
                    (categoryGuids.Count == 0 || categoryGuids.Contains(c.CategoryId!.Value)) &&
                    (accountGuids.Count == 0 || accountGuids.Contains(c.AccountId!.Value));
            }
            else if (request.Type == TransactionTypes.Budget)
            {
                var budgetGuids = GuidParser.ParseGuids(request.BudgetIds);
                if (budgetGuids == null) return Result.Failure<PageResult<TransactionDto>>(Error.InvalidRequest);

                var personalCategoryGuids = GuidParser.ParseGuids(request.PersonalCategoryIds);
                if (personalCategoryGuids == null) return Result.Failure<PageResult<TransactionDto>>(Error.InvalidRequest);

                filter = c =>
                    c.BudgetId != null &&
                    c.Date >= dateFrom &&
                    c.Date <= dateTo &&
                    c.CreatedById == request.UserId &&
                    (string.IsNullOrEmpty(request.Search) || c.Budget.Name.Contains(request.Search)) &&
                    (budgetGuids.Count == 0 || budgetGuids.Contains(c.BudgetId!.Value)) &&
                    (personalCategoryGuids.Count == 0 || personalCategoryGuids.Contains(c.PersonalCategoryId!.Value));
            }

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