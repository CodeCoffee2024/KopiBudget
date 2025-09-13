using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;
using MediatR;

namespace KopiBudget.Application.Queries.Transaction.GetTransaction
{
    public class GetTransactionQueryHandler(
        ITransactionRepository _transactionRepository,
        IMapper _mapper
    ) : IRequestHandler<GetTransactionQuery, Result<TransactionDto>>
    {
        #region Public Methods

        public async Task<Result<TransactionDto>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id!))
            {
                return Result.Failure<TransactionDto>(Error.NullValue);
            }
            var result = await _transactionRepository.GetByIdAsync(Guid.Parse(request.Id!));

            if (result == null)
            {
                return Result.Failure<TransactionDto>(Error.Notfound("Transaction"));
            }
            return Result.Success(_mapper.Map<TransactionDto>(result));
        }

        #endregion Public Methods
    }
}