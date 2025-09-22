using KopiBudget.Api.Middleware.Authorization;
using KopiBudget.Api.Shared;
using KopiBudget.Application.Commands.PersonalCategory.PersonalCategoryDelete;
using KopiBudget.Application.Queries.PersonalCategory.GetPersonalCategories;
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
    public class PersonalCategoryController : ApiBaseController
    {
        #region Public Constructors

        public PersonalCategoryController(ISender sender) : base(sender)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet]
        public async Task<IActionResult> GetPersonalCategories()
        {
            var result = await _sender.Send(new GetPersonalCategoriesQuery(UserId));
            return HandleResponse(result);
        }

        [HttpGet("Dropdown")]
        public async Task<IActionResult> Dropdown([FromQuery] PersonalCategoryRequest request)
        {
            var query = request.ToDropdownQuery(UserId);
            var result = await _sender.Send(query);
            return HandleResponse(result);
        }

        [HttpPost]
        [PermissionAuthorize(Modules.BUDGET, Permissions.MODIFY)]
        public async Task<IActionResult> Create([FromBody] PersonalCategoryRequest request, CancellationToken cancellationToken)
        {
            var command = request.SetAddCommand(UserId);
            var result = await _sender.Send(command, cancellationToken);
            return HandleResponse(result);
        }

        [HttpPut("{id}")]
        [PermissionAuthorize(Modules.BUDGET, Permissions.MODIFY)]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] PersonalCategoryRequest request, CancellationToken cancellationToken)
        {
            var command = request.SetUpdateCommand(UserId, id);
            var result = await _sender.Send(command, cancellationToken);
            return HandleResponse(result);
        }

        [HttpDelete("{id}")]
        [PermissionAuthorize(Modules.BUDGET, Permissions.MODIFY)]
        public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken cancellationToken)
        {
            var command = new PersonalCategoryDeleteCommand(id);
            var result = await _sender.Send(command, cancellationToken);
            return HandleResponse(result);
        }

        #endregion Public Methods
    }
}