using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;
using MediatR;

namespace KopiBudget.Application.Queries.Dashboard.GetDashboardBalanceExpenses
{
    public class GetDashboardBalanceExpensesQueryHandler(
        ITransactionRepository _transactionRepository
    ) : IRequestHandler<GetDashboardBalanceExpensesQuery, Result<IEnumerable<DashboardBalanceExpenseDto>>>
    {
        #region Public Methods

        public async Task<Result<IEnumerable<DashboardBalanceExpenseDto>>> Handle(GetDashboardBalanceExpensesQuery request, CancellationToken cancellationToken)
        {
            var balance = await _transactionRepository.GetBalanceAsync(DateTime.UtcNow.Year, DateTime.UtcNow.Month, request.UserId);
            var expenses = await _transactionRepository.GetExpenseAsync(DateTime.UtcNow.Year, DateTime.UtcNow.Month, request.UserId);
            int previousYear = DateTime.UtcNow.Month == 1 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;
            int previousMonth = DateTime.UtcNow.Month == 1 ? 12 : DateTime.UtcNow.Month - 1;
            var prevousBalance = await _transactionRepository.GetBalanceAsync(previousYear, previousMonth, request.UserId);
            var prevousExpenses = await _transactionRepository.GetExpenseAsync(previousYear, previousMonth, request.UserId);
            IEnumerable<DashboardBalanceExpenseDto> resultHeader = new[]
            {
                new DashboardBalanceExpenseDto { Label = "Balance", Value = balance,
                    PreviousMonth = (prevousBalance != 0)
                        ? (balance / prevousBalance) * 100
                        : 0 },
                new DashboardBalanceExpenseDto { Label = "Expense", Value = expenses,
                    PreviousMonth = (prevousBalance != 0)
                        ? (balance / prevousBalance) * 100
                        : 0 },
            };
            return Result.Success(resultHeader);
        }

        #endregion Public Methods
    }
}