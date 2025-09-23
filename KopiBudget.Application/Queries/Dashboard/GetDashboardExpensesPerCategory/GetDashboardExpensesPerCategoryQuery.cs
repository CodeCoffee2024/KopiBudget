using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.Dashboard.GetDashboardExpensesPerCategory
{
    public class GetDashboardExpensesPerCategoryQuery : IRequest<Result<IEnumerable<DashboardSummaryDto>>>
    {
        #region Public Constructors

        public GetDashboardExpensesPerCategoryQuery(Guid userId)
        {
            UserId = userId;
        }

        #endregion Public Constructors

        #region Properties

        public Guid UserId { get; set; }

        #endregion Properties
    }
}