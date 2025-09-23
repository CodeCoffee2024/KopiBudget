using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;
using MediatR;

namespace KopiBudget.Application.Queries.Dashboard.GetDashboardExpensesPerPersonalCategory
{
    public class GetDashboardExpensesPerPersonalCategoryQueryHandler(
        ITransactionRepository _transactionRepository
    ) : IRequestHandler<GetDashboardExpensesPerPersonalCategoryQuery, Result<IEnumerable<DashboardSummaryDto>>>
    {
        #region Public Methods

        public async Task<Result<IEnumerable<DashboardSummaryDto>>> Handle(GetDashboardExpensesPerPersonalCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _transactionRepository.GetAmountPerPersonalCategory(request.UserId);
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