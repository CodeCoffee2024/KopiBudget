using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.Budget.GetBudgets
{
    public record GetBudgetsQuery : IRequest<Result<IEnumerable<BudgetDto>>>
    {
    }
}