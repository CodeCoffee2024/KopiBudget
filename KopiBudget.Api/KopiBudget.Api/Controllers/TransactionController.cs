using KopiBudget.Api.Middleware.Authorization;
using KopiBudget.Api.Shared;
using KopiBudget.Application.Commands.Transaction.TransactionDelete;
using KopiBudget.Application.Queries.Transaction.GetTransaction;
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
        [PermissionAuthorize(Modules.TRASACTION, Permissions.VIEW)]
        public async Task<IActionResult> Get([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetTransactionQuery(id), cancellationToken);
            return HandleResponse(result);
        }

        [HttpGet("transactions")]
        [PermissionAuthorize(Modules.TRASACTION, Permissions.VIEW)]
        public async Task<IActionResult> GetTransactions([FromQuery] TransactionRequest request, CancellationToken cancellationToken)
        {
            var req = request.ToQuery();
            var result = await _sender.Send(req, cancellationToken);
            return HandleResponse(result);
        }

        [HttpPut("{id}")]
        [PermissionAuthorize(Modules.TRASACTION, Permissions.MODIFY)]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] TransactionRequest request, CancellationToken cancellationToken)
        {
            var command = request.SetUpdateCommand(UserId, id);
            var result = await _sender.Send(command, cancellationToken);
            return HandleResponse(result);
        }

        [HttpDelete("{id}")]
        [PermissionAuthorize(Modules.TRASACTION, Permissions.MODIFY)]
        public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken cancellationToken)
        {
            var command = new TransactionDeleteCommand(id);
            var result = await _sender.Send(command, cancellationToken);
            return HandleResponse(result);
        }

        #endregion Public Methods
    }
}