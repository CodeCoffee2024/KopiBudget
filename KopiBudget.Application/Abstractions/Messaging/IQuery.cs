using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}