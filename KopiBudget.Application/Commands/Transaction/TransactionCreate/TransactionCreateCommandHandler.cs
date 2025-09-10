using AutoMapper;
using FluentValidation;
using KopiBudget.Application.Abstractions.Messaging;
using KopiBudget.Application.Dtos;
using KopiBudget.Application.Extensions;
using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;

namespace KopiBudget.Application.Commands.Transaction.TransactionCreate
{
    internal sealed class TransactionCreateCommandHandler(
        ITransactionRepository _repository,
        IValidator<TransactionCreateCommand> _validator,
        IMapper _mapper,
        IUnitOfWork _unitOfWork
    ) : ICommandHandler<TransactionCreateCommand, TransactionDto>
    {
        #region Public Methods

        public async Task<Result<TransactionDto>> Handle(TransactionCreateCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Result.Failure<TransactionDto>(Error.Validation, validationResult.ToErrorList());
            }

            var entity = KopiBudget.Domain.Entities.Transaction.Create(request.Amount, this.CombineDateAndTimeUtc(request.Date, request.Time), request.CategoryId, request.AccountId, request.Note, request.UserId, DateTime.UtcNow);
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(_mapper.Map<TransactionDto>(entity));
        }
        public DateTime CombineDateAndTimeUtc(DateTime? date, string? time)
        {
            if (date == null)
                throw new ArgumentNullException(nameof(date), "Date is required.");

            if (string.IsNullOrWhiteSpace(time))
                time = "00:00";

            if (!TimeSpan.TryParse(time, out var timeSpan))
                throw new FormatException($"Invalid time format: {time}");

            var localDateTime = date.Value.Date.Add(timeSpan);

            return DateTime.SpecifyKind(localDateTime, DateTimeKind.Local).ToUniversalTime();
        }
        #endregion Public Methods
    }
}