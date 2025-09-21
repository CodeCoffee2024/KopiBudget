using KopiBudget.Api.Middleware.Authorization;
using KopiBudget.Api.Shared;
using KopiBudget.Application.Commands.BudgetPersonalCategoryCmd.BudgetPersonalCategoryDelete;
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
    public class BudgetPersonalCategoryController : ApiBaseController
    {
        #region Public Constructors

        public BudgetPersonalCategoryController(ISender sender) : base(sender)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPut("{budgetId}/{personalCategoryId}")]
        [PermissionAuthorize(Modules.BUDGET, Permissions.MODIFY)]
        public async Task<IActionResult> Update([FromRoute] string budgetId, [FromRoute] string personalCategoryId, [FromBody] BudgetPersonalCategoryRequest request, CancellationToken cancellationToken)
        {
            var command = request.SetUpdateCommand(UserId, budgetId, personalCategoryId);
            var result = await _sender.Send(command, cancellationToken);
            return HandleResponse(result);
        }

        [HttpDelete("{budgetId}/{personalCategoryId}")]
        [PermissionAuthorize(Modules.BUDGET, Permissions.MODIFY)]
        public async Task<IActionResult> Delete([FromRoute] string budgetId, [FromRoute] string personalCategoryId, CancellationToken cancellationToken)
        {
            var command = new BudgetPersonalCategoryDeleteCommand(budgetId, personalCategoryId);
            var result = await _sender.Send(command, cancellationToken);
            return HandleResponse(result);
        }

        #endregion Public Methods
    }
}