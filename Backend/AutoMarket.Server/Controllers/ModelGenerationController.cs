using Microsoft.AspNetCore.Mvc;

namespace AutoMarket.Server.Controllers
{
    public class ModelGenerationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
