using Microsoft.AspNetCore.Mvc;

namespace KopiBudget.Api.Controllers
{
    public class TransactionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
