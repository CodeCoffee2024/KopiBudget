using KopiBudget.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KopiBudget.Api.Controllers
{
    [ApiController]
    [Route("api/debug")]
    public class DebugController : ControllerBase
    {
        #region Fields

        private readonly AppDbContext _context;

        #endregion Fields

        #region Public Constructors

        public DebugController(AppDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpGet("database-info")]
        public async Task<IActionResult> GetDatabaseInfo()
        {
            try
            {
                var databaseName = _context.Database.GetDbConnection().Database;
                var dataSource = _context.Database.GetDbConnection().DataSource;
                var canConnect = await _context.Database.CanConnectAsync();

                return Ok(new
                {
                    DatabaseName = databaseName,
                    DataSource = dataSource,
                    CanConnect = canConnect,
                    PendingMigrations = (await _context.Database.GetPendingMigrationsAsync()).ToArray(),
                    AppliedMigrations = (await _context.Database.GetAppliedMigrationsAsync()).ToArray()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("tables")]
        public async Task<IActionResult> GetTables()
        {
            try
            {
                var tables = await _context.Database.SqlQueryRaw<string>(
                    "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'"
                ).ToListAsync();

                return Ok(tables);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        #endregion Public Methods
    }
}