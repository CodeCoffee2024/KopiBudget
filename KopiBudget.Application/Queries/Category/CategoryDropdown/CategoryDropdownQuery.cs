using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.Category.CategoryDropdown
{
    public class GetCategoryDropdownQuery : IRequest<Result<PageResult<CategoryDropdownDto>>>
    {
        #region Public Constructors

        public GetCategoryDropdownQuery(string search, int pageNumber = 1)
        {
            Search = search;
            PageNumber = pageNumber;
        }

        #endregion Public Constructors

        #region Properties

        public string Search { get; set; }
        public int PageNumber { get; set; }

        #endregion Properties
    }
}