using KopiBudget.Api.Shared;
using KopiBudget.Application.Queries.System.GetCurrencyList;
using KopiBudget.Application.Queries.System.GetModuleGroupList;
using KopiBudget.Application.Queries.System.GetPersonalCategoryIconList;
using KopiBudget.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("GetCurrencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            var result = await _sender.Send(new GetCurrencyListQuery());
            return HandleResponse(result);
        }

        [HttpGet("GetPersonalCategoryFields")]
        public async Task<IActionResult> GetPersonalCategoryFields()
        {
            var result = await _sender.Send(new GetPersonalCategoryIconListQuery());
            return HandleResponse(result);
        }

        [HttpGet("DropdownCurrency")]
        public async Task<IActionResult> DropdownCurrency([FromQuery] SystemSettingsRequest request)
        {
            var result = await _sender.Send(request.ToDropdownQuery());
            return HandleResponse(result);
        }

        [Authorize]
        [HttpPut("UpdateCurrency")]
        public async Task<IActionResult> UpdateCurrency([FromBody] SystemSettingsRequest request)
        {
            var result = await _sender.Send(request.SetUpdateCurrencyCommand(UserId));
            return HandleResponse(result);
        }

        #endregion Public Methods
    }
}