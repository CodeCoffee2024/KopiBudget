using KopiBudget.Api.Data;
using KopiBudget.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KopiBudget.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        #region Fields

        private readonly AppDbContext _context;

        #endregion Fields

        #region Public Constructors

        public TransactionsController(AppDbContext context) => _context = context;

        #endregion Public Constructors

        #region Public Methods

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _context.Transactions.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Post(FinanceTransaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = transaction.Id }, transaction);
        }

        #endregion Public Methods
    }
}