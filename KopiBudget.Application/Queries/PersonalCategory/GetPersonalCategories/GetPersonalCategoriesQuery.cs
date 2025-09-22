using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.PersonalCategory.GetPersonalCategories
{
    public record GetPersonalCategoriesQuery : IRequest<Result<IEnumerable<PersonalCategoryDto>>>
    {
        public GetPersonalCategoriesQuery(Guid userId)
        {
            UserId = userId;
        }
        public Guid UserId { get; set; }
    }
}