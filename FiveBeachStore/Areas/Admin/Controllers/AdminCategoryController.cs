using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiveBeachStore.Models;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SQLitePCL;
using PagedList.Core;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace FiveBeachStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCategoryController : Controller
    {
        private readonly FiveBeachStoreContext _context;
        public INotyfService _notifyServive { get; }
        public AdminCategoryController(FiveBeachStoreContext context, INotyfService notifyServive)
        {
            _context = context;
            _notifyServive = notifyServive;
        }

        // GET: Admin/AdminCategory
        public async Task<IActionResult> Index(int ? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 1;
            var lsCategory = _context.TbCategories.AsNoTracking()
                //.Include(x => x.ParentId)
                .Where(m => m.Status != 0)
                .OrderByDescending(x => x.Id);
            PagedList<TbCategory> models = new PagedList<TbCategory>(lsCategory, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
                         


        }

        // GET: Admin/AdminCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbCategories == null)
            {
                return NotFound();
            }

            var tbCategory = await _context.TbCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbCategory == null)
            {
                return NotFound();
            }

            return View(tbCategory);
        }

        // GET: Admin/AdminCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Slug,ParentId,SortOrder,Level,Image,Metakey,Metadesc,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbCategory tbCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbCategory);
                await _context.SaveChangesAsync();
                _notifyServive.Success("Tạo mới danh mục sản phẩm thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbCategory);
        }

        // GET: Admin/AdminCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbCategories == null)
            {
                return NotFound();
            }

            var tbCategory = await _context.TbCategories.FindAsync(id);
            if (tbCategory == null)
            {
                return NotFound();
            }
            return View(tbCategory);
        }

        // POST: Admin/AdminCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Slug,ParentId,SortOrder,Level,Image,Metakey,Metadesc,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbCategory tbCategory)
        {
            if (id != tbCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbCategoryExists(tbCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _notifyServive.Success("Chỉnh sửa danh mục sản phẩm thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbCategory);
        }

        // GET: Admin/AdminCategory/Destroy/5
        public async Task<IActionResult> Destroy(int? id)
        {
            if (id == null || _context.TbCategories == null)
            {
                return NotFound();
            }

            var tbCategory = await _context.TbCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbCategory == null)
            {
                return NotFound();
            }

            return View(tbCategory);
        }

        // POST: Admin/AdminCategory/Destroy/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DestroyConfirmed(int id)
        {
            if (_context.TbCategories == null)
            {
                return Problem("Entity set 'FiveBeachStoreContext.TbCategories'  is null.");
            }
            var tbCategory = await _context.TbCategories.FindAsync(id);
            if (tbCategory != null)
            {
                _context.TbCategories.Remove(tbCategory);
            }
            
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa danh mục sản phẩm thành công");
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AdminRole/Delete/5
        // Xóa vào thùng rác Status==0
        public async Task<IActionResult> Delete(int? id)
        {
            var tbCategory = await _context.TbCategories.FindAsync(id);
            tbCategory.Status = 0;
            _context.Update(tbCategory);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa danh mục sản phẩm vào thùng rác thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Status/5
        // Thay đổi trạng thái Status
        public async Task<IActionResult> Status(int? id)
        {
            var tbCategory = await _context.TbCategories.FindAsync(id);
            int v = (tbCategory.Status == 2) ? 1 : 2;
            tbCategory.Status = (byte?)v;
            tbCategory.UpdatedAt = DateTime.Now;
            tbCategory.UpdatedBy = 1;
            _context.Update(tbCategory);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Thay đổi trạng thái danh mục sản phẩm thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Restore/5
        //Khôi phục Status==2
        public async Task<IActionResult> Restore(int? id)
        {
            var tbCategory = await _context.TbCategories.FindAsync(id);
            tbCategory.Status = 2;
            _context.Update(tbCategory);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Hoàn tác danh mục sản phẩm thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Trash/5
        //Hiện danh sách của quản lý quyền truy cập
        public async Task<IActionResult> Trash()
        {
            return _context.TbCategories != null ?
                        View(await _context.TbCategories.Where(m => m.Status == 0).ToListAsync()) :
                        Problem("Entity set 'FiveBeachStoreContext.TbCategories'  is null.");
        }
        private bool TbCategoryExists(int id)
        {
          return (_context.TbCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
