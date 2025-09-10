using KopiBudget.Api.Shared;
using KopiBudget.Application.Queries.ExchangeRate.GetExchangeRates;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KopiBudget.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ExchangeRatesController : ApiBaseController
    {
        #region Public Constructors

        public ExchangeRatesController(ISender sender) : base(sender)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet]
        public async Task<IActionResult> GetRates()
        {
            var result = await _sender.Send(new GetExchangeRatesQuery());
            return HandleResponse(result);
        }

        #endregion Public Methods
    }
}