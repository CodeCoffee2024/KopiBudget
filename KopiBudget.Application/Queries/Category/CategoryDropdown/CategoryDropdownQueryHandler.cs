using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace KopiBudget.Application.Queries.Category.CategoryDropdown
{
    public class CategoryDropdownQueryHandler(
        ICategoryRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<CategoryDropdownQuery, Result<PageResult<CategoryDropdownDto>>>
    {
        #region Public Methods

        public async Task<Result<PageResult<CategoryDropdownDto>>> Handle(CategoryDropdownQuery request, CancellationToken cancellationToken)
        {
            var excluded = string.IsNullOrEmpty(request.Exclude)
                ? new List<string>()
                : request.Exclude.Split(',').Select(x => x.Trim()).ToList();

            Expression<Func<Domain.Entities.Category, bool>> filter = c =>
                !excluded.Contains(c.Name);

            var pagedResult = await _repository.GetPaginatedCategoriesAsync(
                request.PageNumber,
                10,
                request.Search.ToLower(),
                "name",
                filter
            );

            return Result.Success(
                new PageResult<CategoryDropdownDto>(
                    _mapper.Map<IReadOnlyList<CategoryDropdownDto>>(pagedResult.Items),
                    pagedResult.TotalCount,
                    pagedResult.PageNumber,
                    pagedResult.PageSize,
                    pagedResult.OrderBy)
                );
        }

        #endregion Public Methods
    }
}