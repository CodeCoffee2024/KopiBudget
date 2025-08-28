using KopiBudget.Api.Shared;
using KopiBudget.Application.Requests;
using KopiBudget.Domain.Abstractions;
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

        [HttpPost("UpdateProfile/{id}")]
        public async Task<IActionResult> UpdateProfile([FromRoute] string id, [FromForm] UserRequest request, CancellationToken cancellationToken)
        {
            if (Guid.TryParse(id, out var userId))
            {
                var command = request.SetUpdateUserProfileCommand(userId, userId);
                var result = await _sender.Send(command, cancellationToken);
                return HandleResponse(result);
            }
            return HandleResponse(Result.Failure(Error.NullValue));
        }

        #endregion Public Methods
    }
}