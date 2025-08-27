using KopiBudget.Api.Shared;
using KopiBudget.Application.Queries.System.GetModuleGroupList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KopiBudget.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemController : ApiBaseController
    {
        #region Public Methods

        public SystemController(ISender sender) : base(sender)
        {
        }

        [HttpGet("GetModuleGroups")]
        public async Task<IActionResult> GetAllGroups()
        {
            var result = await _sender.Send(new GetModuleGroupListQuery());
            return HandleResponse(result);
        }

        #endregion Public Methods
    }
}