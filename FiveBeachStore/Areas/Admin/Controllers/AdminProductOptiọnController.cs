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
    public class AdminProductOptiọnController : Controller
    {
        private readonly FiveBeachStoreContext _context;
        public INotyfService _notifyServive { get; }
        public AdminProductOptiọnController(FiveBeachStoreContext context, INotyfService notifyServive)
        {
            _context = context;
            _notifyServive = notifyServive;
        }

        // GET: Admin/AdminProductOptiọn
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 1;
            var lsProductOption = _context.TbProductOptiọns.AsNoTracking()
                //.Include(x => x.ParentId)
                .Where(m => m.Status != 0)
                .OrderByDescending(x => x.ProductId);
            PagedList<TbProductOptiọn> models = new PagedList<TbProductOptiọn>(lsProductOption, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }
            // GET: Admin/AdminProductOptiọn/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbProductOptiọns == null)
            {
                return NotFound();
            }

            var tbProductOptiọn = await _context.TbProductOptiọns
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tbProductOptiọn == null)
            {
                return NotFound();
            }

            return View(tbProductOptiọn);
        }

        // GET: Admin/AdminProductOptiọn/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminProductOptiọn/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Color,Memorysize,Qty,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbProductOptiọn tbProductOptiọn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbProductOptiọn);
                await _context.SaveChangesAsync();
                _notifyServive.Success("Tạo mới Option sản phẩm thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbProductOptiọn);
        }

        // GET: Admin/AdminProductOptiọn/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbProductOptiọns == null)
            {
                return NotFound();
            }

            var tbProductOptiọn = await _context.TbProductOptiọns.FindAsync(id);
            if (tbProductOptiọn == null)
            {
                return NotFound();
            }
            return View(tbProductOptiọn);
        }

        // POST: Admin/AdminProductOptiọn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Color,Memorysize,Qty,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbProductOptiọn tbProductOptiọn)
        {
            if (id != tbProductOptiọn.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbProductOptiọn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbProductOptiọnExists(tbProductOptiọn.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _notifyServive.Success("Chỉnh sửa Option sản phẩm thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbProductOptiọn);
        }

        // GET: Admin/AdminProductOptiọn/Destroy/5
        public async Task<IActionResult> Destroy(int? id)
        {
            if (id == null || _context.TbProductOptiọns == null)
            {
                return NotFound();
            }

            var tbProductOptiọn = await _context.TbProductOptiọns
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tbProductOptiọn == null)
            {
                return NotFound();
            }

            return View(tbProductOptiọn);
        }

        // POST: Admin/AdminProductOptiọn/Destroy/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DestroyConfirmed(int id)
        {
            if (_context.TbProductOptiọns == null)
            {
                return Problem("Entity set 'FiveBeachStoreContext.TbProductOptiọns'  is null.");
            }
            var tbProductOptiọn = await _context.TbProductOptiọns.FindAsync(id);
            if (tbProductOptiọn != null)
            {
                _context.TbProductOptiọns.Remove(tbProductOptiọn);
            }
            
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa Option sản phẩm thành công");
            return RedirectToAction(nameof(Index));
        }
        // GET: Admin/AdminRole/Delete/5
        // Xóa vào thùng rác Status==0
        public async Task<IActionResult> Delete(int? id)
        {
            var tbProductOption = await _context.TbProductOptiọns.FindAsync(id);
            tbProductOption.Status = 0;
            _context.Update(tbProductOption);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa Option sản phẩm vào thùng rác thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Status/5
        // Thay đổi trạng thái Status
        public async Task<IActionResult> Status(int? id)
        {
            var tbProductOption = await _context.TbProductOptiọns.FindAsync(id);
            int v = (tbProductOption.Status == 2) ? 1 : 2;
            tbProductOption.Status = (byte?)v;
            tbProductOption.UpdatedAt = DateTime.Now;
            tbProductOption.UpdatedBy = 1;
            _context.Update(tbProductOption);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Thay đổi trạng thái Option sản phẩm thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Restore/5
        //Khôi phục Status==2
        public async Task<IActionResult> Restore(int? id)
        {
            var tbProductOption = await _context.TbProductOptiọns.FindAsync(id);
            tbProductOption.Status = 2;
            _context.Update(tbProductOption);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Hoàn tác Option sản phẩm thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Trash/5
        //Hiện danh sách của quản lý quyền truy cập
        public async Task<IActionResult> Trash()
        {
            return _context.TbProductOptiọns != null ?
                        View(await _context.TbProductOptiọns.Where(m => m.Status == 0).ToListAsync()) :
                        Problem("Entity set 'FiveBeachStoreContext.TbProductOptiọns'  is null.");
        }
        private bool TbProductOptiọnExists(int id)
        {
          return (_context.TbProductOptiọns?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
