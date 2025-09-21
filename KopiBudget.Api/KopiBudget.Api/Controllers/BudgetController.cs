using KopiBudget.Api.Middleware.Authorization;
using KopiBudget.Api.Shared;
using KopiBudget.Application.Commands.Budget.BudgetDelete;
using KopiBudget.Application.Queries.Budget.GetBudgets;
using KopiBudget.Application.Requests;
using KopiBudget.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KopiBudget.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BudgetController : ApiBaseController
    {
        #region Public Constructors

        public BudgetController(ISender sender) : base(sender)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        [PermissionAuthorize(Modules.BUDGET, Permissions.MODIFY)]
        public async Task<IActionResult> Create([FromBody] BudgetRequest request, CancellationToken cancellationToken)
        {
            var command = request.SetAddCommand(UserId);
            var result = await _sender.Send(command, cancellationToken);
            return HandleResponse(result);
        }

        [HttpGet("Dropdown")]
        public async Task<IActionResult> Dropdown([FromQuery] BudgetRequest request)
        {
            var query = request.ToDropdownQuery();
            var result = await _sender.Send(query);
            return HandleResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBudgets()
        {
            var result = await _sender.Send(new GetBudgetsQuery());
            return HandleResponse(result);
        }

        [HttpPut("{id}")]
        [PermissionAuthorize(Modules.BUDGET, Permissions.MODIFY)]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] BudgetRequest request, CancellationToken cancellationToken)
        {
            var command = request.SetUpdateCommand(UserId, id);
            var result = await _sender.Send(command, cancellationToken);
            return HandleResponse(result);
        }

        [HttpDelete("{id}")]
        [PermissionAuthorize(Modules.BUDGET, Permissions.MODIFY)]
        public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken cancellationToken)
        {
            var command = new BudgetDeleteCommand(id);
            var result = await _sender.Send(command, cancellationToken);
            return HandleResponse(result);
        }

        #endregion Public Methods
    }
}