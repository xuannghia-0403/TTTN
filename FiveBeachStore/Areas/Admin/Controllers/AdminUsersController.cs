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
using Microsoft.AspNetCore.Mvc.RazorPages;
using SQLitePCL;
using PagedList.Core;

namespace FiveBeachStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUsersController : Controller
    {
        private readonly FiveBeachStoreContext _context;
        public INotyfService _notifyServive { get; }
        public AdminUsersController(FiveBeachStoreContext context, INotyfService notifyServive)
        {
            _context = context;
            _notifyServive = notifyServive;
        }

        // GET: Admin/AdminUsers
        public IActionResult Index(int ?page)
        {
            ViewData["QuyenTruyCap"] = new SelectList(_context.TbRoles, "Id", "Metadesc");
            List<SelectListItem> lsTrangThai = new List<SelectListItem>();
            lsTrangThai.Add(new SelectListItem() { Text = "On", Value = "1" });
            lsTrangThai.Add(new SelectListItem() { Text = "Off", Value = "2" });
            ViewData["lsTrangThai"] = lsTrangThai;
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize =1;
            var lsUsers = _context.TbUsers.AsNoTracking()
                .Where(m => m.Status != 0)
                .OrderByDescending(x => x.CreatedAt);
            PagedList<TbUser> models = new PagedList<TbUser>(lsUsers, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
            
        }

        // GET: Admin/AdminUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbUsers == null)
            {
                return NotFound();
            }

            var tbUser = await _context.TbUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbUser == null)
            {
                return NotFound();
            }

            return View(tbUser);
        }

        // GET: Admin/AdminUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone,Username,Password,Address,Image,Roles,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbUser tbUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbUser);
                await _context.SaveChangesAsync();
                _notifyServive.Success("Tạo mới tài khoản thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbUser);
        }

        // GET: Admin/AdminUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbUsers == null)
            {
                return NotFound();
            }

            var tbUser = await _context.TbUsers.FindAsync(id);
            if (tbUser == null)
            {
                return NotFound();
            }
            return View(tbUser);
        }

        // POST: Admin/AdminUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone,Username,Password,Address,Image,Roles,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbUser tbUser)
        {
            if (id != tbUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbUserExists(tbUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _notifyServive.Success("Cập nhật tài khoản thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbUser);
        }

        // GET: Admin/AdminUsers/Delete/5
        public async Task<IActionResult> Destroy(int? id)
        {
            if (id == null || _context.TbUsers == null)
            {
                return NotFound();
            }

            var tbUser = await _context.TbUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbUser == null)
            {
                return NotFound();
            }

            return View(tbUser);
        }

        // POST: Admin/AdminUsers/Destroy/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DestroyConfirmed(int id)
        {
            if (_context.TbUsers == null)
            {
                return Problem("Entity set 'FiveBeachStoreContext.TbUsers'  is null.");
            }
            var tbUser = await _context.TbUsers.FindAsync(id);
            if (tbUser != null)
            {
                _context.TbUsers.Remove(tbUser);
            }
            
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa tài khoản thành công");
            return RedirectToAction(nameof(Index));
        }
        // GET: Admin/AdminRole/Delete/5
        // Xóa vào thùng rác Status==0
        public async Task<IActionResult> Delete(int? id)
        {
            var tbUser = await _context.TbUsers.FindAsync(id);
            tbUser.Status = 0;
            _context.Update(tbUser);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa tài khoản vào thùng rác thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Status/5
        // Thay đổi trạng thái Status
        public async Task<IActionResult> Status(int? id)
        {
            var tbUser = await _context.TbUsers.FindAsync(id);
            int v = (tbUser.Status == 2) ? 1 : 2;
            tbUser.Status = (byte?)v;
            tbUser.UpdatedAt = DateTime.Now;
            tbUser.UpdatedBy = 1;
            _context.Update(tbUser);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Thay đổi trạng thái tài khoản thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Restore/5
        //Khôi phục Status==2
        public async Task<IActionResult> Restore(int? id)
        {
            var tbUser = await _context.TbUsers.FindAsync(id);
            tbUser.Status = 2;
            _context.Update(tbUser);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Hoàn tác tài khoản thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Trash/5
        //Hiện danh sách của quản lý quyền truy cập
        public async Task<IActionResult> Trash()
        {
            return _context.TbUsers != null ?
                        View(await _context.TbUsers.Where(m => m.Status == 0).ToListAsync()) :
                        Problem("Entity set 'FiveBeachStoreContext.TbUsers'  is null.");
        }
        private bool TbUserExists(int id)
        {
          return (_context.TbUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
