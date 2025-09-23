using KopiBudget.Api.Shared;
using KopiBudget.Application.Queries.Dashboard.GetDashboardBalanceExpenses;
using KopiBudget.Application.Queries.Dashboard.GetDashboardExpensesPerCategory;
using KopiBudget.Application.Queries.Dashboard.GetDashboardExpensesPerPersonalCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KopiBudget.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class DashboardController : ApiBaseController
    {
        #region Public Constructors

        public DashboardController(ISender sender) : base(sender)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet("GetDashboardBalanceExpenses")]
        public async Task<IActionResult> GetDashboardBalanceExpenses()
        {
            var result = await _sender.Send(new GetDashboardBalanceExpensesQuery(UserId));
            return HandleResponse(result);
        }

        [HttpGet("GetDashboardExpensesPerCategory")]
        public async Task<IActionResult> GetDashboardExpensesPerCategory()
        {
            var result = await _sender.Send(new GetDashboardExpensesPerCategoryQuery(UserId));
            return HandleResponse(result);
        }

        [HttpGet("GetDashboardExpensesPerPersonalCategory")]
        public async Task<IActionResult> GetDashboardExpensesPerPersonalCategory()
        {
            var result = await _sender.Send(new GetDashboardExpensesPerPersonalCategoryQuery(UserId));
            return HandleResponse(result);
        }

        #endregion Public Methods
    }
}