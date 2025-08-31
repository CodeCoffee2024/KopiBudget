using KopiBudget.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KopiBudget.Api.Shared
{
    [ApiController]
    public abstract class ApiBaseController : ControllerBase
    {
        #region Fields

        protected readonly ISender _sender;

        #endregion Fields

        #region Public Constructors

        public ApiBaseController(ISender sender) => _sender = sender;

        protected Guid UserId
        {
            get
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return Guid.TryParse(userId, out var id)
                    ? id
                    : throw new UnauthorizedAccessException("Invalid or missing user ID in token.");
            }
        }

        protected IActionResult HandleResponse<T>(Result<T> result)
        {
            if (!result.IsSuccess)
            {
                return result.Error switch
                {
                    { StatusCode: StatusCodes.Status404NotFound } => NotFound(result),
                    { StatusCode: StatusCodes.Status400BadRequest } => BadRequest(result),
                    { StatusCode: StatusCodes.Status409Conflict } => Conflict(result),
                    { StatusCode: StatusCodes.Status401Unauthorized } => Unauthorized(result),
                    { StatusCode: StatusCodes.Status403Forbidden } => Forbid(),
                    _ => StatusCode(result.Error?.StatusCode ?? StatusCodes.Status500InternalServerError, result)
                };
            }

            return result.Data != null ? Ok(result) : NotFound(result);
        }


        protected IActionResult HandleResponse(Result result)
        {
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        #endregion Public Constructors
    }
}