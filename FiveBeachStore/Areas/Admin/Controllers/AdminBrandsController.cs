using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiveBeachStore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PagedList.Core;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace FiveBeachStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminBrandsController : Controller
    {
        private readonly FiveBeachStoreContext _context;
        public INotyfService _notifyServive { get; }
        public AdminBrandsController(FiveBeachStoreContext context, INotyfService notifyServive)
        {
            _context = context;
            _notifyServive = notifyServive;
        }

        // GET: Admin/AdminBrands
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 1;
            var lsBrand = _context.TbBrands.AsNoTracking()
                //.Include(x => x.ParentId)
                .Where(m => m.Status != 0)
                .OrderByDescending(x => x.Id);
            PagedList<TbBrand> models = new PagedList<TbBrand>(lsBrand, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminBrands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbBrands == null)
            {
                return NotFound();
            }

            var tbBrand = await _context.TbBrands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbBrand == null)
            {
                return NotFound();
            }

            return View(tbBrand);
        }

        // GET: Admin/AdminBrands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminBrands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Slug,Image,SortOrder,Metakey,Metadesc,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbBrand tbBrand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbBrand);
                await _context.SaveChangesAsync();
                _notifyServive.Success("Tạo mới banner thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbBrand);
        }

        // GET: Admin/AdminBrands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbBrands == null)
            {
                return NotFound();
            }

            var tbBrand = await _context.TbBrands.FindAsync(id);
            if (tbBrand == null)
            {
                return NotFound();
            }
            return View(tbBrand);
        }

        // POST: Admin/AdminBrands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Slug,Image,SortOrder,Metakey,Metadesc,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbBrand tbBrand)
        {
            if (id != tbBrand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbBrand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbBrandExists(tbBrand.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _notifyServive.Success("Chỉnh sửa banner thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbBrand);
        }

        // GET: Admin/AdminBrands/Destroy/5
        public async Task<IActionResult> Destroy(int? id)
        {
            if (id == null || _context.TbBrands == null)
            {
                return NotFound();
            }

            var tbBrand = await _context.TbBrands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbBrand == null)
            {
                return NotFound();
            }

            return View(tbBrand);
        }

        // POST: Admin/AdminBrands/Destroy/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DestroyConfirmed(int id)
        {
            if (_context.TbBrands == null)
            {
                return Problem("Entity set 'FiveBeachStoreContext.TbBrands'  is null.");
            }
            var tbBrand = await _context.TbBrands.FindAsync(id);
            if (tbBrand != null)
            {
                _context.TbBrands.Remove(tbBrand);
            }
            
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa banner thành công");
            return RedirectToAction(nameof(Index));
        }
        // GET: Admin/AdminRole/Delete/5
        // Xóa vào thùng rác Status==0
        public async Task<IActionResult> Delete(int? id)
        {
            var tbbrand = await _context.TbBrands.FindAsync(id);
            tbbrand.Status = 0;
            _context.Update(tbbrand);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa banner vào thùng rác thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Status/5
        // Thay đổi trạng thái Status
        public async Task<IActionResult> Status(int? id)
        {
            var tbbrand = await _context.TbBrands.FindAsync(id);
            int v = (tbbrand.Status == 2) ? 1 : 2;
            tbbrand.Status = (byte?)v;
            tbbrand.UpdatedAt = DateTime.Now;
            tbbrand.UpdatedBy = 1;
            _context.Update(tbbrand);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Thay đổi trạng thái banner thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Restore/5
        //Khôi phục Status==2
        public async Task<IActionResult> Restore(int? id)
        {
            var tbbrand = await _context.TbBrands.FindAsync(id);
            tbbrand.Status = 2;
            _context.Update(tbbrand);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Hoàn tác banner thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Trash/5
        //Hiện danh sách của quản lý quyền truy cập
        public async Task<IActionResult> Trash()
        {
            return _context.TbBrands != null ?
                        View(await _context.TbBrands.Where(m => m.Status == 0).ToListAsync()) :
                        Problem("Entity set 'FiveBeachStoreContext.TbBrands'  is null.");
        }
        private bool TbBrandExists(int id)
        {
          return (_context.TbBrands?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
