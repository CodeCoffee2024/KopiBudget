using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;
using MediatR;

namespace KopiBudget.Application.Queries.Dashboard.GetDashboardExpensesPerCategory
{
    public class GetDashboardExpensesPerCategoryQueryHandler(
        ITransactionRepository _transactionRepository
    ) : IRequestHandler<GetDashboardExpensesPerCategoryQuery, Result<IEnumerable<DashboardSummaryDto>>>
    {
        #region Public Methods

        public async Task<Result<IEnumerable<DashboardSummaryDto>>> Handle(GetDashboardExpensesPerCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _transactionRepository.GetAmountPerCategory(request.UserId);
            var dtoList = result.Select(r => new DashboardSummaryDto
            {
                Label = r.label,
                Value = r.value
            });
            return Result.Success(dtoList);
        }

        #endregion Public Methods
    }
}