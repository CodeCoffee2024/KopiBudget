using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace KopiBudget.Application.Queries.Account.AccountDropdown
{
    public class AccountDropdownQueryHandler(
        IAccountRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<AccountDropdownQuery, Result<PageResult<AccountDropdownDto>>>
    {
        #region Public Methods

        public async Task<Result<PageResult<AccountDropdownDto>>> Handle(AccountDropdownQuery request, CancellationToken cancellationToken)
        {
            var excluded = string.IsNullOrEmpty(request.Exclude)
                ? new List<string>()
                : request.Exclude.Split(',').Select(x => x.Trim()).ToList();

            Expression<Func<Domain.Entities.Account, bool>> filter = c =>
                !excluded.Contains(c.Name);

            var pagedResult = await _repository.GetPaginatedCategoriesAsync(
                request.PageNumber,
                10,
                request.Search.ToLower(),
                "name",
                filter
            );

            return Result.Success(
                new PageResult<AccountDropdownDto>(
                    _mapper.Map<IReadOnlyList<AccountDropdownDto>>(pagedResult.Items),
                    pagedResult.TotalCount,
                    pagedResult.PageNumber,
                    pagedResult.PageSize,
                    pagedResult.OrderBy)
                );
        }

        #endregion Public Methods
    }
}