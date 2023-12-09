using Microsoft.AspNetCore.Mvc;

namespace FiveBeachStore.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
