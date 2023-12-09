using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiveBeachStore.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PagedList.Core;

namespace FiveBeachStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductSalesController : Controller
    {

        private readonly FiveBeachStoreContext _context;
        public INotyfService _notifyServive { get; }
        public AdminProductSalesController(FiveBeachStoreContext context, INotyfService notifyServive)
        {
            _context = context;
            _notifyServive = notifyServive;
        }

        // GET: Admin/AdminProductSales
        public async Task<IActionResult> Index(int? page)
        {

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 1;
            var lsProductSale = _context.TbProductSales.AsNoTracking()
                //.Include(x => x.ParentId)
                .Where(m => m.Status != 0)
                .OrderByDescending(x => x.ProductId);
            PagedList<TbProductSale> models = new PagedList<TbProductSale>(lsProductSale, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminProductSales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbProductSales == null)
            {
                return NotFound();
            }

            var tbProductSale = await _context.TbProductSales
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tbProductSale == null)
            {
                return NotFound();
            }

            return View(tbProductSale);
        }

        // GET: Admin/AdminProductSales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminProductSales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,PriceSale,Qty,NgayBd,NgayKt,Status")] TbProductSale tbProductSale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbProductSale);
                await _context.SaveChangesAsync();
                _notifyServive.Success("Tạo mới giá sản phẩm Sale thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbProductSale);
        }

        // GET: Admin/AdminProductSales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbProductSales == null)
            {
                return NotFound();
            }

            var tbProductSale = await _context.TbProductSales.FindAsync(id);
            if (tbProductSale == null)
            {
                return NotFound();
            }
            return View(tbProductSale);
        }

        // POST: Admin/AdminProductSales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,PriceSale,Qty,NgayBd,NgayKt,Status")] TbProductSale tbProductSale)
        {
            if (id != tbProductSale.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbProductSale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbProductSaleExists(tbProductSale.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            _notifyServive.Success("Cập nhật giá sản phẩm Sale thành công");
            return View(tbProductSale);
        }

        // GET: Admin/AdminProductSales/Destroy/5
        public async Task<IActionResult> Destroy(int? id)
        {
            if (id == null || _context.TbProductSales == null)
            {
                return NotFound();
            }

            var tbProductSale = await _context.TbProductSales
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tbProductSale == null)
            {
                return NotFound();
            }

            return View(tbProductSale);
        }

        // POST: Admin/AdminProductSales/Destroy/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DestroyConfirmed(int id)
        {
            if (_context.TbProductSales == null)
            {
                return Problem("Entity set 'FiveBeachStoreContext.TbProductSales'  is null.");
            }
            var tbProductSale = await _context.TbProductSales.FindAsync(id);
            if (tbProductSale != null)
            {
                _context.TbProductSales.Remove(tbProductSale);
            }
            
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa giá sản phẩm Sale thành công");
            return RedirectToAction(nameof(Index));
        }
        // GET: Admin/AdminRole/Delete/5
        // Xóa vào thùng rác Status==0
        public async Task<IActionResult> Delete(int? id)
        {
            var tbProductImage = await _context.TbProductSales.FindAsync(id);
            tbProductImage.Status = 0;
            _context.Update(tbProductImage);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa hình ảnh sản phẩm vào thùng rác thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Status/5
        // Thay đổi trạng thái Status
        public async Task<IActionResult> Status(int? id)
        {
            var tbProductSale = await _context.TbProductSales.FindAsync(id);
            int v = (tbProductSale.Status == 2) ? 1 : 2;
            tbProductSale.Status = (byte?)v;
           
            _context.Update(tbProductSale);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Thay đổi trạng thái giá sản phẩm sale thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Restore/5
        //Khôi phục Status==2
        public async Task<IActionResult> Restore(int? id)
        {
            var tbProductSale = await _context.TbProductSales.FindAsync(id);
            tbProductSale.Status = 2;
            _context.Update(tbProductSale);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Hoàn tác giá sản phẩm sale thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Trash/5
        //Hiện danh sách của quản lý quyền truy cập
        public async Task<IActionResult> Trash()
        {
            return _context.TbProductSales != null ?
                        View(await _context.TbProductSales.Where(m => m.Status == 0).ToListAsync()) :
                        Problem("Entity set 'FiveBeachStoreContext.TbProductSales'  is null.");
        }
        private bool TbProductSaleExists(int id)
        {
          return (_context.TbProductSales?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
