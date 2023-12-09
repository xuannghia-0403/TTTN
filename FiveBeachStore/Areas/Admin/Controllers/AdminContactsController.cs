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
    public class AdminContactsController : Controller
    {
        private readonly FiveBeachStoreContext _context;
        public INotyfService _notifyServive { get; }
        public AdminContactsController(FiveBeachStoreContext context, INotyfService notifyServive)
        {
            _context = context;
            _notifyServive = notifyServive;
        }

        // GET: Admin/AdminContacts
        public async Task<IActionResult> Index(int ? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 1;
            var lsContact = _context.TbContacts.AsNoTracking()
                //.Include(x => x.ParentId)
                .Where(m => m.Status != 0)
                .OrderByDescending(x => x.Id);
            PagedList<TbContact> models = new PagedList<TbContact>(lsContact, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminContacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbContacts == null)
            {
                return NotFound();
            }

            var tbContact = await _context.TbContacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbContact == null)
            {
                return NotFound();
            }

            return View(tbContact);
        }

        // GET: Admin/AdminContacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminContacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Name,Email,Phone,Title,Content,ReplayId,CreatedAt,UpdatedAt,UpdatedBy,Status")] TbContact tbContact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbContact);
                await _context.SaveChangesAsync();
                _notifyServive.Success("Tạo mới liên hệ thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbContact);
        }

        // GET: Admin/AdminContacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbContacts == null)
            {
                return NotFound();
            }

            var tbContact = await _context.TbContacts.FindAsync(id);
            if (tbContact == null)
            {
                return NotFound();
            }
            return View(tbContact);
        }

        // POST: Admin/AdminContacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Name,Email,Phone,Title,Content,ReplayId,CreatedAt,UpdatedAt,UpdatedBy,Status")] TbContact tbContact)
        {
            if (id != tbContact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbContact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbContactExists(tbContact.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _notifyServive.Success("Cập nhật liên hệ thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbContact);
        }

        // GET: Admin/AdminContacts/Destroy/5
        public async Task<IActionResult> Destroy(int? id)
        {
            if (id == null || _context.TbContacts == null)
            {
                return NotFound();
            }

            var tbContact = await _context.TbContacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbContact == null)
            {
                return NotFound();
            }

            return View(tbContact);
        }

        // POST: Admin/AdminContacts/Destroy/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DestroyConfirmed(int id)
        {
            if (_context.TbContacts == null)
            {
                return Problem("Entity set 'FiveBeachStoreContext.TbContacts'  is null.");
            }
            var tbContact = await _context.TbContacts.FindAsync(id);
            if (tbContact != null)
            {
                _context.TbContacts.Remove(tbContact);
            }
            
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa liên hệ thành công");
            return RedirectToAction(nameof(Index));
        }
        // GET: Admin/AdminRole/Delete/5
        // Xóa vào thùng rác Status==0
        public async Task<IActionResult> Delete(int? id)
        {
            var tbContact = await _context.TbContacts.FindAsync(id);
            tbContact.Status = 0;
            _context.Update(tbContact);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa liên hệ vào thùng rác thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Status/5
        // Thay đổi trạng thái Status
        public async Task<IActionResult> Status(int? id)
        {
            var tbContact = await _context.TbContacts.FindAsync(id);
            int v = (tbContact.Status == 2) ? 1 : 2;
            tbContact.Status = (byte?)v;
            tbContact.UpdatedAt = DateTime.Now;
            tbContact.UpdatedBy = 1;
            _context.Update(tbContact);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Thay đổi trạng thái liên hệ thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Restore/5
        //Khôi phục Status==2
        public async Task<IActionResult> Restore(int? id)
        {
            var tbContact = await _context.TbContacts.FindAsync(id);
            tbContact.Status = 2;
            _context.Update(tbContact);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Hoàn tác liên hệ thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Trash/5
        //Hiện danh sách của quản lý quyền truy cập
        public async Task<IActionResult> Trash()
        {
            return _context.TbContacts != null ?
                        View(await _context.TbContacts.Where(m => m.Status == 0).ToListAsync()) :
                        Problem("Entity set 'FiveBeachStoreContext.TbContacts'  is null.");
        }
        private bool TbContactExists(int id)
        {
          return (_context.TbContacts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
