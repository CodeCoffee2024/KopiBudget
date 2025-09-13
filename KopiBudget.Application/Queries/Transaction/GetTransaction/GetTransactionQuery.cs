using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.Transaction.GetTransaction
{
    public class GetTransactionQuery : IRequest<Result<TransactionDto>>
    {
        #region Public Constructors

        public GetTransactionQuery(string? id)
        {
            Id = id;
        }

        #endregion Public Constructors

        #region Properties

        public string? Id { get; set; } = string.Empty;

        #endregion Properties
    }
}