using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;
using MediatR;

namespace KopiBudget.Application.Queries.Budget.GetBudgets
{
    public class GetBudgetsQueryHandler(IBudgetRepository _repository, IMapper _mapper) : IRequestHandler<GetBudgetsQuery, Result<IEnumerable<BudgetDto>>>
    {
        #region Public Methods

        public async Task<Result<IEnumerable<BudgetDto>>> Handle(GetBudgetsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetAllByUserIdAsync(request.UserId);
                var mappedResult = _mapper.Map<IEnumerable<BudgetDto>>(result);
                return Result.Success(mappedResult);
            }
            catch (AutoMapperMappingException ex)
            {
                // Log the detailed error
                Console.WriteLine($"Mapping error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                return Result.Failure<IEnumerable<BudgetDto>>(Error.InvalidRequest);
            }
        }

        #endregion Public Methods
    }
}