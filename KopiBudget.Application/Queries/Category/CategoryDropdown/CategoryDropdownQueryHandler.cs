using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;
using MediatR;

namespace KopiBudget.Application.Queries.Category.CategoryDropdown
{
    public class CategoryDropdownQueryHandler(
        ICategoryRepository _repository,
        IMapper _mapper
    ) : IRequestHandler<GetCategoryDropdownQuery, Result<PageResult<CategoryDropdownDto>>>
    {
        #region Public Methods

        public async Task<Result<PageResult<CategoryDropdownDto>>> Handle(GetCategoryDropdownQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _repository.GetPaginatedCategoriesAsync(
                request.PageNumber, 10, request.Search, "name", null);

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