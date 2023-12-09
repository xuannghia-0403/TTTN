using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiveBeachStore.Models;
using System.Runtime.CompilerServices;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace FiveBeachStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRoleController : Controller
    {
        private readonly FiveBeachStoreContext _context;
        public INotyfService _notifyServive { get; }

        public AdminRoleController(FiveBeachStoreContext context, INotyfService notifyServive)
        {
            _context = context;
            _notifyServive = notifyServive;
        }

        // GET: Admin/AdminRole
        public async Task<IActionResult> Index()
        {
              return _context.TbRoles != null ? 
                      
                           View(await _context.TbRoles.Where(m => m.Status !=0).ToListAsync()):
                          Problem("Entity set 'FiveBeachStoreContext.TbRoles'  is null.");
        }

        // GET: Admin/AdminRole/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbRoles == null)
            {
                return NotFound();
            }

            var tbRole = await _context.TbRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbRole == null)
            {
                return NotFound();
            }

            return View(tbRole);
        }

        // GET: Admin/AdminRole/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminRole/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Metadesc,Status")] TbRole tbRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbRole);
                await _context.SaveChangesAsync();
                _notifyServive.Success("Tạo mới quyèn truy cập thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbRole);
        }

        // GET: Admin/AdminRole/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbRoles == null)
            {
                return NotFound();
            }

            var tbRole = await _context.TbRoles.FindAsync(id);
            if (tbRole == null)
            {
                return NotFound();
            }
            return View(tbRole);
        }

        // POST: Admin/AdminRole/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Metadesc,Status")] TbRole tbRole)
        {
            if (id != tbRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbRoleExists(tbRole.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _notifyServive.Success("Cập nhật quyền truy cập thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbRole);
        }

        // GET: Admin/AdminRole/Destroy/5
        public async Task<IActionResult> Destroy(int? id)
        {
            if (id == null || _context.TbRoles == null)
            {
                return NotFound();
            }

            var tbRole = await _context.TbRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbRole == null)
            {
                return NotFound();
            }

            return View("Destroy",tbRole);
        }

        // POST: Admin/AdminRole/Destroy/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DestroyConfirmed(int id)
        {
            if (_context.TbRoles == null)
            {
                return Problem("Entity set 'FiveBeachStoreContext.TbRoles'  is null.");
            }
            var tbRole = await _context.TbRoles.FindAsync(id);
            if (tbRole != null)
            {
                _context.TbRoles.Remove(tbRole);
            }
            
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa quyền truy cập thành công");
            return RedirectToAction(nameof(Trash));
        }
        // GET: Admin/AdminRole/Delete/5
        // Xóa vào thùng rác Status==0
        public async Task<IActionResult> Delete(int? id)
        {
            var tbRole = await _context.TbRoles.FindAsync(id);
            tbRole.Status = 0;
            _context.Update(tbRole);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa quyền truy cập vào thùng rác thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Status/5
        // Thay đổi trạng thái Status
        public async Task<IActionResult> Status(int? id)
        {
            var tbRole = await _context.TbRoles.FindAsync(id);
            int v = (tbRole.Status == 2) ? 1 : 2;
            tbRole.Status = (byte?)v;
            //tbRole.Updated_At = DateTime.Now;
            //tbRole.Updated_By = 1;
            _context.Update(tbRole);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Thay đổi trạng thái quyền truy cập thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Restore/5
        //Khôi phục Status==2
        public async Task<IActionResult> Restore(int? id)
        {
            var tbRole = await _context.TbRoles.FindAsync(id);
            tbRole.Status = 2;
            _context.Update(tbRole);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Hoàn tác quyền truy cập thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Trash/5
        //Hiện danh sách của quản lý quyền truy cập
        public async Task<IActionResult> Trash()
        {
            return _context.TbRoles != null ? 
                        View(await _context.TbRoles.Where(m => m.Status == 0).ToListAsync()) :
                        Problem("Entity set 'FiveBeachStoreContext.TbRoles'  is null.");
        }
        private bool TbRoleExists(int id)
        {
          return (_context.TbRoles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
