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
    public class AdminOrdersController : Controller
    {
        private readonly FiveBeachStoreContext _context;
        public INotyfService _notifyServive { get; }
        public AdminOrdersController(FiveBeachStoreContext context, INotyfService notifyServive)
        {
            _context = context;
            _notifyServive = notifyServive;
        }

        // GET: Admin/AdminOrders
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 1;
            var lsOrder = _context.TbOrders.AsNoTracking()
                //.Include(x => x.ParentId)
                .Where(m => m.Status != 0)
                .OrderByDescending(x => x.Id);
            PagedList<TbOrder> models = new PagedList<TbOrder>(lsOrder, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbOrders == null)
            {
                return NotFound();
            }

            var tbOrder = await _context.TbOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbOrder == null)
            {
                return NotFound();
            }

            return View(tbOrder);
        }

        // GET: Admin/AdminOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Name,Phone,Email,Address,Note,CreatedAt,UpdatedAt,UpdatedBy,Status")] TbOrder tbOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbOrder);
                await _context.SaveChangesAsync();
                _notifyServive.Success("Tạo mới đơn hàng thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbOrder);
        }

        // GET: Admin/AdminOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbOrders == null)
            {
                return NotFound();
            }

            var tbOrder = await _context.TbOrders.FindAsync(id);
            if (tbOrder == null)
            {
                return NotFound();
            }
            return View(tbOrder);
        }

        // POST: Admin/AdminOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Name,Phone,Email,Address,Note,CreatedAt,UpdatedAt,UpdatedBy,Status")] TbOrder tbOrder)
        {
            if (id != tbOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbOrderExists(tbOrder.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _notifyServive.Success("Cập nhật đơn hàng thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbOrder);
        }

        // GET: Admin/AdminOrders/Destroy/5
        public async Task<IActionResult> Destroy(int? id)
        {
            if (id == null || _context.TbOrders == null)
            {
                return NotFound();
            }

            var tbOrder = await _context.TbOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbOrder == null)
            {
                return NotFound();
            }

            return View(tbOrder);
        }

        // POST: Admin/AdminOrders/Destroy/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DestroyConfirmed(int id)
        {
            if (_context.TbOrders == null)
            {
                return Problem("Entity set 'FiveBeachStoreContext.TbOrders'  is null.");
            }
            var tbOrder = await _context.TbOrders.FindAsync(id);
            if (tbOrder != null)
            {
                _context.TbOrders.Remove(tbOrder);
            }
            
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa đơn hàng thành công");
            return RedirectToAction(nameof(Index));
        }
        // GET: Admin/AdminRole/Delete/5
        // Xóa vào thùng rác Status==0
        public async Task<IActionResult> Delete(int? id)
        {
            var tbOrder = await _context.TbOrders.FindAsync(id);
            tbOrder.Status = 0;
            _context.Update(tbOrder);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa đơn hàng vào thùng rác thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Status/5
        // Thay đổi trạng thái Status
        public async Task<IActionResult> Status(int? id)
        {
            var tbOrder = await _context.TbOrders.FindAsync(id);
            int v = (tbOrder.Status == 2) ? 1 : 2;
            tbOrder.Status = (byte?)v;
            tbOrder.UpdatedAt = DateTime.Now;
            tbOrder.UpdatedBy = 1;
            _context.Update(tbOrder);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Thay đổi trạng thái đơn hàng thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Restore/5
        //Khôi phục Status==2
        public async Task<IActionResult> Restore(int? id)
        {
            var tbOrder = await _context.TbOrders.FindAsync(id);
            tbOrder.Status = 2;
            _context.Update(tbOrder);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Hoàn tác đơn hàng thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Trash/5
        //Hiện danh sách của quản lý quyền truy cập
        public async Task<IActionResult> Trash()
        {
            return _context.TbOrders != null ?
                        View(await _context.TbOrders.Where(m => m.Status == 0).ToListAsync()) :
                        Problem("Entity set 'FiveBeachStoreContext.TbOrders'  is null.");
        }
        private bool TbOrderExists(int id)
        {
          return (_context.TbOrders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
