using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.System.GetCurrencyList
{
    public class GetCurrencyListQuery() : IRequest<Result<IEnumerable<CurrencyDto>>>
    {
    }
}