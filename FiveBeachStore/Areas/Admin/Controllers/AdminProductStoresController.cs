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
    public class AdminProductStoresController : Controller
    {
        private readonly FiveBeachStoreContext _context;
        public INotyfService _notifyServive { get; }
        public AdminProductStoresController(FiveBeachStoreContext context, INotyfService notifyServive)
        {
            _context = context;
            _notifyServive = notifyServive;
        }

        // GET: Admin/AdminProductStores
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 1;
            var lsProductStore = _context.TbProductStores.AsNoTracking()
                //.Include(x => x.ParentId)
                .Where(m => m.Status != 0)
                .OrderByDescending(x => x.ProductId);
            PagedList<TbProductStore> models = new PagedList<TbProductStore>(lsProductStore, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminProductStores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbProductStores == null)
            {
                return NotFound();
            }

            var tbProductStore = await _context.TbProductStores
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tbProductStore == null)
            {
                return NotFound();
            }

            return View(tbProductStore);
        }

        // GET: Admin/AdminProductStores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminProductStores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Qty,Price,Metadesc,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbProductStore tbProductStore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbProductStore);
                await _context.SaveChangesAsync();
                _notifyServive.Success("Tạo mới số lượng sản phẩm thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbProductStore);
        }

        // GET: Admin/AdminProductStores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbProductStores == null)
            {
                return NotFound();
            }

            var tbProductStore = await _context.TbProductStores.FindAsync(id);
            if (tbProductStore == null)
            {
                return NotFound();
            }
            return View(tbProductStore);
        }

        // POST: Admin/AdminProductStores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Qty,Price,Metadesc,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbProductStore tbProductStore)
        {
            if (id != tbProductStore.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbProductStore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbProductStoreExists(tbProductStore.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _notifyServive.Success("Cập nhật số lượng sản phẩm thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbProductStore);
        }

        // GET: Admin/AdminProductStores/Destroy/5
        public async Task<IActionResult> Destroy(int? id)
        {
            if (id == null || _context.TbProductStores == null)
            {
                return NotFound();
            }

            var tbProductStore = await _context.TbProductStores
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tbProductStore == null)
            {
                return NotFound();
            }

            return View(tbProductStore);
        }

        // POST: Admin/AdminProductStores/Destroy/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DestroyConfirmed(int id)
        {
            if (_context.TbProductStores == null)
            {
                return Problem("Entity set 'FiveBeachStoreContext.TbProductStores'  is null.");
            }
            var tbProductStore = await _context.TbProductStores.FindAsync(id);
            if (tbProductStore != null)
            {
                _context.TbProductStores.Remove(tbProductStore);
            }
            
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa số lượng sản phẩm thành công");
            return RedirectToAction(nameof(Index));
        }
        // GET: Admin/AdminRole/Delete/5
        // Xóa vào thùng rác Status==0
        public async Task<IActionResult> Delete(int? id)
        {
            var tbProductStore = await _context.TbProductStores.FindAsync(id);
            tbProductStore.Status = 0;
            _context.Update(tbProductStore);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa số lượng sản phẩm vào thùng rác thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Status/5
        // Thay đổi trạng thái Status
        public async Task<IActionResult> Status(int? id)
        {
            var tbProductStore = await _context.TbProductStores.FindAsync(id);
            int v = (tbProductStore.Status == 2) ? 1 : 2;
            tbProductStore.Status = (byte?)v;
            tbProductStore.UpdatedAt = DateTime.Now;
            tbProductStore.UpdatedBy = 1;
            _context.Update(tbProductStore);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Thay đổi trạng thái số lượng sản phẩm thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Restore/5
        //Khôi phục Status==2
        public async Task<IActionResult> Restore(int? id)
        {
            var tbProductStore = await _context.TbProductStores.FindAsync(id);
            tbProductStore.Status = 2;
            _context.Update(tbProductStore);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Hoàn tác số lượng sản phẩm thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Trash/5
        //Hiện danh sách của quản lý quyền truy cập
        public async Task<IActionResult> Trash()
        {
            return _context.TbProductStores != null ?
                        View(await _context.TbProductStores.Where(m => m.Status == 0).ToListAsync()) :
                        Problem("Entity set 'FiveBeachStoreContext.TbProductStores'  is null.");
        }
        private bool TbProductStoreExists(int id)
        {
          return (_context.TbProductStores?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
