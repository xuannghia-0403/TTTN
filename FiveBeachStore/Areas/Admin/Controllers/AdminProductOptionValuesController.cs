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
    public class AdminProductOptionValuesController : Controller
    {
        private readonly FiveBeachStoreContext _context;
        public INotyfService _notifyServive { get; }
        public AdminProductOptionValuesController(FiveBeachStoreContext context, INotyfService notifyServive)
        {
            _context = context;
            _notifyServive = notifyServive;
        }

        // GET: Admin/AdminProductOptionValues
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 1;
            var lsProductOptionValue = _context.TbProductOptionValues.AsNoTracking()
                //.Include(x => x.ParentId)
                .Where(m => m.Status != 0)
                .OrderByDescending(x => x.ProductId);
            PagedList<TbProductOptionValue> models = new PagedList<TbProductOptionValue>(lsProductOptionValue, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminProductOptionValues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbProductOptionValues == null)
            {
                return NotFound();
            }

            var tbProductOptionValue = await _context.TbProductOptionValues
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tbProductOptionValue == null)
            {
                return NotFound();
            }

            return View(tbProductOptionValue);
        }

        // GET: Admin/AdminProductOptionValues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminProductOptionValues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Price,Metadesc,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbProductOptionValue tbProductOptionValue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbProductOptionValue);
                await _context.SaveChangesAsync();
                _notifyServive.Success("Tạo mới giá sản phẩm thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbProductOptionValue);
        }

        // GET: Admin/AdminProductOptionValues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbProductOptionValues == null)
            {
                return NotFound();
            }

            var tbProductOptionValue = await _context.TbProductOptionValues.FindAsync(id);
            if (tbProductOptionValue == null)
            {
                return NotFound();
            }
            return View(tbProductOptionValue);
        }

        // POST: Admin/AdminProductOptionValues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Price,Metadesc,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbProductOptionValue tbProductOptionValue)
        {
            if (id != tbProductOptionValue.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbProductOptionValue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbProductOptionValueExists(tbProductOptionValue.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _notifyServive.Success("Cập nhật giá sản phẩm thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbProductOptionValue);
        }

        // GET: Admin/AdminProductOptionValues/Destroy/5
        public async Task<IActionResult> Destroy(int? id)
        {
            if (id == null || _context.TbProductOptionValues == null)
            {
                return NotFound();
            }

            var tbProductOptionValue = await _context.TbProductOptionValues
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tbProductOptionValue == null)
            {
                return NotFound();
            }

            return View(tbProductOptionValue);
        }

        // POST: Admin/AdminProductOptionValues/Destroy/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DestroyConfirmed(int id)
        {
            if (_context.TbProductOptionValues == null)
            {
                return Problem("Entity set 'FiveBeachStoreContext.TbProductOptionValues'  is null.");
            }
            var tbProductOptionValue = await _context.TbProductOptionValues.FindAsync(id);
            if (tbProductOptionValue != null)
            {
                _context.TbProductOptionValues.Remove(tbProductOptionValue);
            }
            
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa giá sản phẩm thành công");
            return RedirectToAction(nameof(Index));
        }
        // GET: Admin/AdminRole/Delete/5
        // Xóa vào thùng rác Status==0
        public async Task<IActionResult> Delete(int? id)
        {
            var tbProductOptionValue = await _context.TbProductOptionValues.FindAsync(id);
            tbProductOptionValue.Status = 0;
            _context.Update(tbProductOptionValue);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa giá sản phẩm vào thùng rác thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Status/5
        // Thay đổi trạng thái Status
        public async Task<IActionResult> Status(int? id)
        {
            var tbProductOptionValue = await _context.TbProductOptionValues.FindAsync(id);
            int v = (tbProductOptionValue.Status == 2) ? 1 : 2;
            tbProductOptionValue.Status = (byte?)v;
            tbProductOptionValue.UpdatedAt = DateTime.Now;
            tbProductOptionValue.UpdatedBy = 1;
            _context.Update(tbProductOptionValue);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Thay đổi trạng thái giá sản phẩm thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Restore/5
        //Khôi phục Status==2
        public async Task<IActionResult> Restore(int? id)
        {
            var tbProductOptionValue = await _context.TbProductOptionValues.FindAsync(id);
            tbProductOptionValue.Status = 2;
            _context.Update(tbProductOptionValue);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Hoàn tác giá sản phẩm thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Trash/5
        //Hiện danh sách của quản lý quyền truy cập
        public async Task<IActionResult> Trash()
        {
            return _context.TbProductOptionValues != null ?
                        View(await _context.TbProductOptionValues.Where(m => m.Status == 0).ToListAsync()) :
                        Problem("Entity set 'FiveBeachStoreContext.TbProductOptionValues'  is null.");
        }
        private bool TbProductOptionValueExists(int id)
        {
          return (_context.TbProductOptionValues?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
