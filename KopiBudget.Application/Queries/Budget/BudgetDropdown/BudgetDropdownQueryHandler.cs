using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace KopiBudget.Application.Queries.Budget.BudgetDropdown
{
    public class BudgetDropdownQueryHandler(
        IBudgetRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<BudgetDropdownQuery, Result<PageResult<BudgetDropdownDto>>>
    {
        #region Public Methods

        public async Task<Result<PageResult<BudgetDropdownDto>>> Handle(BudgetDropdownQuery request, CancellationToken cancellationToken)
        {
            var excluded = string.IsNullOrEmpty(request.Exclude)
                ? new List<string>()
                : request.Exclude.Split(',').Select(x => x.Trim()).ToList();

            Expression<Func<Domain.Entities.Budget, bool>> filter = c =>
                !excluded.Contains(c.Name) && c.CreatedById == request.UserId;

            var pagedResult = await _repository.GetPaginatedBudgetsAsync(
                request.PageNumber,
                10,
                request.Search.ToLower(),
                "name",
                filter
            );

            return Result.Success(
                new PageResult<BudgetDropdownDto>(
                    _mapper.Map<IReadOnlyList<BudgetDropdownDto>>(pagedResult.Items),
                    pagedResult.TotalCount,
                    pagedResult.PageNumber,
                    pagedResult.PageSize,
                    pagedResult.OrderBy)
                );
        }

        #endregion Public Methods
    }
}