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
    public class AdminProductImagesController : Controller
    {
        private readonly FiveBeachStoreContext _context;
        public INotyfService _notifyServive { get; }
        public AdminProductImagesController(FiveBeachStoreContext context, INotyfService notifyServive)
        {
            _context = context;
            _notifyServive = notifyServive;
        }

        // GET: Admin/AdminProductImages
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 1;
            var lsProductImage = _context.TbProductImages.AsNoTracking()
                //.Include(x => x.ParentId)
                .Where(m => m.Status != 0)
                .OrderByDescending(x => x.ProductId);
            PagedList<TbProductImage> models = new PagedList<TbProductImage>(lsProductImage, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminProductImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbProductImages == null)
            {
                return NotFound();
            }

            var tbProductImage = await _context.TbProductImages
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tbProductImage == null)
            {
                return NotFound();
            }

            return View(tbProductImage);
        }

        // GET: Admin/AdminProductImages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminProductImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Image,Metadesc,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbProductImage tbProductImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbProductImage);
                await _context.SaveChangesAsync();
                _notifyServive.Success("Tạo mới hình ảnh sản phẩm thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbProductImage);
        }

        // GET: Admin/AdminProductImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbProductImages == null)
            {
                return NotFound();
            }

            var tbProductImage = await _context.TbProductImages.FindAsync(id);
            if (tbProductImage == null)
            {
                return NotFound();
            }
            return View(tbProductImage);
        }

        // POST: Admin/AdminProductImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Image,Metadesc,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbProductImage tbProductImage)
        {
            if (id != tbProductImage.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbProductImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbProductImageExists(tbProductImage.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _notifyServive.Success("Chỉnh sửa hình ảnh sản phẩm thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbProductImage);
        }

        // GET: Admin/AdminProductImages/Destroy/5
        public async Task<IActionResult> Destroy(int? id)
        {
            if (id == null || _context.TbProductImages == null)
            {
                return NotFound();
            }

            var tbProductImage = await _context.TbProductImages
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tbProductImage == null)
            {
                return NotFound();
            }

            return View(tbProductImage);
        }

        // POST: Admin/AdminProductImages/Destroy/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DestroyConfirmed(int id)
        {
            if (_context.TbProductImages == null)
            {
                return Problem("Entity set 'FiveBeachStoreContext.TbProductImages'  is null.");
            }
            var tbProductImage = await _context.TbProductImages.FindAsync(id);
            if (tbProductImage != null)
            {
                _context.TbProductImages.Remove(tbProductImage);
            }
            
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa hình ảnh sản phẩm thành công");
            return RedirectToAction(nameof(Index));
        }
        // GET: Admin/AdminRole/Delete/5
        // Xóa vào thùng rác Status==0
        public async Task<IActionResult> Delete(int? id)
        {
            var tbProductImage = await _context.TbProductImages.FindAsync(id);
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
            var tbProductImage = await _context.TbProductImages.FindAsync(id);
            int v = (tbProductImage.Status == 2) ? 1 : 2;
            tbProductImage.Status = (byte?)v;
            tbProductImage.UpdatedAt = DateTime.Now;
            tbProductImage.UpdatedBy = 1;
            _context.Update(tbProductImage);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Thay đổi trạng thái hình ảnh sản phẩm thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Restore/5
        //Khôi phục Status==2
        public async Task<IActionResult> Restore(int? id)
        {
            var tbProductImage = await _context.TbProductImages.FindAsync(id);
            tbProductImage.Status = 2;
            _context.Update(tbProductImage);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Hoàn tác hình ảnh sản phẩm thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Trash/5
        //Hiện danh sách của quản lý quyền truy cập
        public async Task<IActionResult> Trash()
        {
            return _context.TbProductImages != null ?
                        View(await _context.TbProductImages.Where(m => m.Status == 0).ToListAsync()) :
                        Problem("Entity set 'FiveBeachStoreContext.TbProductImages'  is null.");
        }
        private bool TbProductImageExists(int id)
        {
          return (_context.TbProductImages?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
