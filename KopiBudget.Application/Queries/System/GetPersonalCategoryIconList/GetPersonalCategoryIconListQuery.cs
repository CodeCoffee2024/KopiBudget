using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.System.GetPersonalCategoryIconList
{
    public class GetPersonalCategoryIconListQuery() : IRequest<Result<PersonalCategoryFieldDto>>
    {
    }
}