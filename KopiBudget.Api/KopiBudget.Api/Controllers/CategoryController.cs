using KopiBudget.Api.Middleware.Authorization;
using KopiBudget.Api.Shared;
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
    public class CategoryController : ApiBaseController
    {
        #region Public Constructors

        public CategoryController(ISender sender) : base(sender)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        //[HttpGet("GetCategories")]
        //[PermissionAuthorize(Modules.CATEGORY, Permissions.VIEW)]
        //public async Task<IActionResult> GetListing([FromQuery] CategoryRequest request, CancellationToken cancellationToken)
        //{
        //    var query = request.ToQuery();
        //    var result = await _sender.Send(query, cancellationToken);
        //    return HandleResponse(result);
        //}

        //[HttpDelete("{id}")]
        //[PermissionAuthorize(Modules.CATEGORY, Permissions.MODIFY)]
        //public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        //{
        //    var command = new DeleteCategoryCommand(id);
        //    var result = await _sender.Send(command, cancellationToken);
        //    return HandleResponse(result);
        //}

        //[HttpGet("GetStatuses")]
        //[PermissionAuthorize(Modules.CATEGORY, Permissions.VIEW)]
        //public async Task<IActionResult> GetStatuses()
        //{
        //    var result = await _sender.Send(new GetRoleStatusesQuery());
        //    return HandleResponse(result);
        //}

        [HttpGet("Dropdown")]
        public async Task<IActionResult> Dropdown([FromQuery] CategoryRequest request)
        {
            var query = request.ToDropdownQuery(UserId);
            var result = await _sender.Send(query);
            return HandleResponse(result);
        }

        [HttpPost]
        [PermissionAuthorize(Modules.CATEGORY, Permissions.MODIFY)]
        public async Task<IActionResult> Create([FromBody] CategoryRequest request, CancellationToken cancellationToken)
        {
            var command = request.SetAddCommand(UserId);
            var result = await _sender.Send(command, cancellationToken);
            return HandleResponse(result);
        }

        //[HttpGet("{id}")]
        //[PermissionAuthorize(Modules.CATEGORY, Permissions.VIEW)]
        //public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        //{
        //    var command = new GetOneCategoryQuery(id);
        //    var result = await _sender.Send(command, cancellationToken);
        //    return HandleResponse(result);
        //}

        #endregion Public Methods
    }
}