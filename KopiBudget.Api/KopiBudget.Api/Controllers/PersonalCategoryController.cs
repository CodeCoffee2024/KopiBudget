using KopiBudget.Api.Shared;
using KopiBudget.Application.Queries.PersonalCategory.GetPersonalCategories;
using KopiBudget.Application.Requests;
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
            var result = await _sender.Send(new GetPersonalCategoriesQuery());
            return HandleResponse(result);
        }

        [HttpGet("Dropdown")]
        public async Task<IActionResult> Dropdown([FromQuery] PersonalCategoryRequest request)
        {
            var query = request.ToDropdownQuery(UserId);
            var result = await _sender.Send(query);
            return HandleResponse(result);
        }

        #endregion Public Methods
    }
}