using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace KopiBudget.Application.Queries.PersonalCategory.PersonalCategoryDropdown
{
    public class PersonalCategoryDropdownQueryHandler(
        IPersonalCategoryRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<PersonalCategoryDropdownQuery, Result<PageResult<PersonalCategoryDropdownDto>>>
    {
        #region Public Methods

        public async Task<Result<PageResult<PersonalCategoryDropdownDto>>> Handle(PersonalCategoryDropdownQuery request, CancellationToken cancellationToken)
        {
            var excluded = string.IsNullOrEmpty(request.Exclude)
                ? new List<string>()
                : request.Exclude.Split(',').Select(x => x.Trim()).ToList();

            Expression<Func<Domain.Entities.PersonalCategory, bool>> filter = c =>
                !excluded.Contains(c.Name) && c.CreatedById == request.UserId;
            if (request.BudgetIds.Count() == 0)
            {
                var pagedResult = await _repository.GetPaginatedPersonalCategoriesByBudgetIdAsync(
                    request.PageNumber,
                    10,
                    request.Search.ToLower(),
                    "name",
                    request.BudgetId
                );

                return Result.Success(
                    new PageResult<PersonalCategoryDropdownDto>(
                        _mapper.Map<IReadOnlyList<PersonalCategoryDropdownDto>>(pagedResult.Items),
                        pagedResult.TotalCount,
                        pagedResult.PageNumber,
                        pagedResult.PageSize,
                        pagedResult.OrderBy)
                    );
            }
            else
            {
                var pagedResult = await _repository.GetPaginatedPersonalCategoriesByBudgetIdsAsync(
                    request.PageNumber,
                    10,
                    request.Search.ToLower(),
                    "name",
                    request.BudgetIds
                );

                return Result.Success(
                    new PageResult<PersonalCategoryDropdownDto>(
                        _mapper.Map<IReadOnlyList<PersonalCategoryDropdownDto>>(pagedResult.Items),
                        pagedResult.TotalCount,
                        pagedResult.PageNumber,
                        pagedResult.PageSize,
                        pagedResult.OrderBy)
                    );
            }
        }

        #endregion Public Methods
    }
}