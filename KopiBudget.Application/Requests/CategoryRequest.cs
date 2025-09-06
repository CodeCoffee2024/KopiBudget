using KopiBudget.Application.Abstractions.Request;
using KopiBudget.Application.Commands.Category.CategoryCreate;
using KopiBudget.Application.Queries.Category.CategoryDropdown;

namespace KopiBudget.Application.Requests
{
    public class CategoryRequest : PageRequest
    {
        #region Properties

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        #endregion Properties

        #region Public Methods

        public CategoryCreateCommand SetAddCommand(Guid UserId) =>
            new(UserId, Name);

        //public GetCategoryListQuery ToQuery() => new(Search, OrderBy, PageNumber, PageSize, Status);

        public GetCategoryDropdownQuery ToDropdownQuery() => new(Search!, PageNumber);

        #endregion Public Methods
    }
}