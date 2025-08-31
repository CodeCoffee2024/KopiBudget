using KopiBudget.Api.Shared;
using KopiBudget.Application.Queries.Auth.AuthUserDetail;
using KopiBudget.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KopiBudget.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ApiBaseController
    {
        #region Public Constructors

        public AuthController(ISender sender) : base(sender)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest request, CancellationToken cancellationToken)
        {
            var query = request.LoginQuery();
            var result = await _sender.Send(query, cancellationToken);
            return HandleResponse(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRequest request, CancellationToken cancellationToken)
        {
            var command = request.SetRegisterCommand();
            var result = await _sender.Send(command, cancellationToken);
            return HandleResponse(result);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] AuthRequest request, CancellationToken cancellationToken)
        {
            var userAccessQuery = request.SetRefreshToken();
            var result = await _sender.Send(userAccessQuery);
            return HandleResponse(result);
        }
        [Authorize]
        [HttpGet("user-detail")]
        public async Task<IActionResult> GetUserDetails(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new AuthUserDetailQuery(UserId));
            return HandleResponse(result);
        }

        #endregion Public Methods
    }
}