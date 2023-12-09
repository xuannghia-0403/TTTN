using Microsoft.AspNetCore.Mvc;

namespace FiveBeachStore.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
