using KopiBudget.Api.Shared;
using KopiBudget.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KopiBudget.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ApiBaseController
    {
        #region Public Constructors

        public UserController(ISender sender) : base(sender)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRequest request, CancellationToken cancellationToken)
        {
            var command = request.SetRegisterCommand();
            var result = await _sender.Send(command, cancellationToken);
            return HandleResponse(result);
        }

        #endregion Public Methods
    }
}