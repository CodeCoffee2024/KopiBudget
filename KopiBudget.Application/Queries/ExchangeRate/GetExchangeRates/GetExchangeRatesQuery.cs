using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.ExchangeRate.GetExchangeRates
{
    public record GetExchangeRatesQuery : IRequest<Result<ExchangeRateDto>>
    {
    }
}