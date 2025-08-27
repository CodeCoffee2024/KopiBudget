using KopiBudget.Application.Dtos;
using KopiBudget.Domain.Abstractions;
using MediatR;

namespace KopiBudget.Application.Queries.System.GetModuleGroupList
{
    public class GetModuleGroupListQuery() : IRequest<Result<IEnumerable<ModuleGroupDto>>>
    {
    }
}