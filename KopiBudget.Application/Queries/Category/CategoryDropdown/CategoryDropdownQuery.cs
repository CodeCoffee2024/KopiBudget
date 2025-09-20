using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.Category.CategoryDropdown
{
    public class CategoryDropdownQuery : IRequest<Result<PageResult<CategoryDropdownDto>>>
    {
        #region Public Constructors

        public CategoryDropdownQuery(string search, int pageNumber = 1, string? exclude = "")
        {
            Search = search;
            PageNumber = pageNumber;
            Exclude = exclude!;
        }

        #endregion Public Constructors

        #region Properties

        public string Search { get; set; }
        public string Exclude { get; set; }
        public int PageNumber { get; set; }

        #endregion Properties
    }
}