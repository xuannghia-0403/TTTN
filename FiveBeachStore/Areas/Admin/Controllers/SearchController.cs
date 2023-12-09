using AspNetCoreHero.ToastNotification.Abstractions;
using FiveBeachStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiveBeachStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private readonly FiveBeachStoreContext _context;
        public SearchController(FiveBeachStoreContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult FindProduct(string keyword)
        {
            List<TbProduct> ls = new List<TbProduct>();
            if(string.IsNullOrEmpty(keyword)|| keyword.Length<1)

            {
                return PartialView("ListProductSearchPartial", null);
            }
            ls = _context.TbProducts.AsNoTracking()
                                    .Include(a => a.Category)
                                    .Where(x => x.Name.Contains(keyword))
                                    .OrderByDescending(x => x.Name)
                                    .Take(10)
                                    .ToList();
            if(ls==null)
            {
                return PartialView("ListProductSearchPartial", null);
            }    
            else
            {
                return PartialView("ListProductSearchPartial", ls);
            }    
        }
    }
}
