using AutoMapper;
using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Interfaces;
using MediatR;

namespace KopiBudget.Application.Queries.PersonalCategory.GetPersonalCategories
{
    public class GetPersonalCategoriesQueryHandler(IPersonalCategoryRepository _repository, IMapper _mapper) : IRequestHandler<GetPersonalCategoriesQuery, Result<IEnumerable<PersonalCategoryDto>>>
    {
        #region Public Methods

        public async Task<Result<IEnumerable<PersonalCategoryDto>>> Handle(GetPersonalCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync();

            return Result.Success(_mapper.Map<IEnumerable<PersonalCategoryDto>>(result));
        }

        #endregion Public Methods
    }
}