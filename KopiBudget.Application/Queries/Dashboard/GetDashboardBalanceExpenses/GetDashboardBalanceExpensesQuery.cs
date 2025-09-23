using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.Dashboard.GetDashboardBalanceExpenses
{
    public class GetDashboardBalanceExpensesQuery : IRequest<Result<IEnumerable<DashboardBalanceExpenseDto>>>
    {
        #region Public Constructors

        public GetDashboardBalanceExpensesQuery(Guid userId)
        {
            UserId = userId;
        }

        #endregion Public Constructors

        #region Properties

        public Guid UserId { get; set; }

        #endregion Properties
    }
}