using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.Dashboard.GetDashboardExpensesPerPersonalCategory
{
    public class GetDashboardExpensesPerPersonalCategoryQuery : IRequest<Result<IEnumerable<DashboardSummaryDto>>>
    {
        #region Public Constructors

        public GetDashboardExpensesPerPersonalCategoryQuery(Guid userId)
        {
            UserId = userId;
        }

        #endregion Public Constructors

        #region Properties

        public Guid UserId { get; set; }

        #endregion Properties
    }
}