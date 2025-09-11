using KopiBudget.Api.Middleware.Authorization;
using KopiBudget.Api.Shared;
using KopiBudget.Application.Queries.Account.GetAccounts;
using KopiBudget.Application.Requests;
using KopiBudget.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KopiBudget.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ApiBaseController
    {
        #region Public Methods

        public TransactionController(ISender sender) : base(sender)
        {
        }

        [HttpPost]
        [PermissionAuthorize(Modules.TRASACTION, Permissions.MODIFY)]
        public async Task<IActionResult> Create([FromBody] TransactionRequest request, CancellationToken cancellationToken)
        {
            var command = request.SetAddCommand(UserId);
            var result = await _sender.Send(command, cancellationToken);
            return HandleResponse(result);
        }

        [HttpGet("{id}")]
        [PermissionAuthorize(Modules.ACCOUNT, Permissions.MODIFY)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetUserAccountQuery(UserId), cancellationToken);
            return HandleResponse(result);
        }

        [HttpGet("transactions")]
        [PermissionAuthorize(Modules.ACCOUNT, Permissions.MODIFY)]
        public async Task<IActionResult> GetTransactions([FromQuery] TransactionRequest request, CancellationToken cancellationToken)
        {
            var req = request.ToQuery();
            var result = await _sender.Send(req, cancellationToken);
            return HandleResponse(result);
        }

        #endregion Public Methods
    }
}