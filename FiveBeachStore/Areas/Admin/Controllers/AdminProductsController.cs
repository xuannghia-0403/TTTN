using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiveBeachStore.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SQLitePCL;
using PagedList.Core;

namespace FiveBeachStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly FiveBeachStoreContext _context;
        public INotyfService _notifyServive { get; }
        public AdminProductsController(FiveBeachStoreContext context, INotyfService notifyServive)
        {
            _context = context;
            _notifyServive = notifyServive;
        }

        // GET: Admin/AdminProducts
        public IActionResult Index(int page=1,int category_id = 0)
        {
            var pageNumber = page; 
            var pageSize = 20;
            List<TbProduct> lsProduct = new List<TbProduct>();
            
            if (category_id != 0)
            {
                lsProduct = _context.TbProducts
               .AsNoTracking()   
               .Where(x=>x.CategoryId== category_id)
               .Where(m => m.Status != 0)
               .Include(x=>x.Category)
               .OrderByDescending(x=>x.Id).ToList();
            }
            else
            {
                lsProduct = _context.TbProducts
               .AsNoTracking()
               .Where(m => m.Status != 0)
               .Include(p => p.Category)
               .OrderByDescending(x => x.Id).ToList();
            }
                
           
            PagedList<TbProduct> models = new PagedList<TbProduct>(lsProduct.AsQueryable(), pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.CurrentCateID = category_id;
            ViewBag.CurrentPage = pageNumber;

            ViewData["DanhMuc"] = new SelectList(_context.TbCategories, "Id", "Name", category_id);
            return View(models);

        }
        public IActionResult Filtter(int category_id = 0)
        {
            var url = $"/Admin/AdminProducts?CategoryId={category_id}";
            if(category_id == 0)
            {
                url = $"/Admin/AdminProducts";
            }
            return Json(new { Status = "0", RedirectUrl = url });
        }

        // GET: Admin/AdminProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbProducts == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.TbProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbProduct == null)
            {
                return NotFound();
            }

            return View(tbProduct);
        }

        // GET: Admin/AdminProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,BrandId,Name,Slug,Price,PriceSale,Image,Qty,Detail,Metakey,Metadesc,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbProduct tbProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbProduct);
                await _context.SaveChangesAsync();
                _notifyServive.Success("Tạo mới sản phẩm thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbProduct);
        }

        // GET: Admin/AdminProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbProducts == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.TbProducts.FindAsync(id);
            if (tbProduct == null)
            {
                return NotFound();
            }
            return View(tbProduct);
        }

        // POST: Admin/AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,BrandId,Name,Slug,Price,PriceSale,Image,Qty,Detail,Metakey,Metadesc,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbProduct tbProduct)
        {
            if (id != tbProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbProductExists(tbProduct.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _notifyServive.Success("Cập nhật sản phẩm thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(tbProduct);
        }

        // GET: Admin/AdminProducts/Destroy/5
        public async Task<IActionResult> Destroy(int? id)
        {
            if (id == null || _context.TbProducts == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.TbProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbProduct == null)
            {
                return NotFound();
            }

            return View(tbProduct);
        }

        // POST: Admin/AdminProducts/Destroy/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DestroyConfirmed(int id)
        {
            if (_context.TbProducts == null)
            {
                return Problem("Entity set 'FiveBeachStoreContext.TbProducts'  is null.");
            }
            var tbProduct = await _context.TbProducts.FindAsync(id);
            if (tbProduct != null)
            {
                _context.TbProducts.Remove(tbProduct);
            }
            
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa sản phẩm thành công");
            return RedirectToAction(nameof(Index));
        }


        // GET: Admin/AdminRole/Delete/5
        // Xóa vào thùng rác Status==0
        public async Task<IActionResult> Delete(int? id)
        {
            var tbProduct = await _context.TbProducts.FindAsync(id);
            tbProduct.Status = 0;
            _context.Update(tbProduct);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa sản phẩm vào thùng rác thành công!");
            return RedirectToAction(nameof(Index));

        }
       

        // GET: Admin/AdminRole/Status/5
        // Thay đổi trạng thái Status
        public async Task<IActionResult> Status(int? id)
        {
            var tbProduct = await _context.TbProducts.FindAsync(id);
            int v = (tbProduct.Status == 2) ? 1 : 2;
            tbProduct.Status = (byte?)v;
            //tbRole.Updated_At = DateTime.Now;
            //tbRole.Updated_By = 1;
            _context.Update(tbProduct);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Thay đổi trạng thái sản phẩm thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Restore/5
        //Khôi phục Status==2
        public async Task<IActionResult> Restore(int? id)
        {
            var tbProduct = await _context.TbProducts.FindAsync(id);
            tbProduct.Status = 2;
            _context.Update(tbProduct);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Hoàn tác sản phẩm thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Trash/5
        //Hiện danh sách của quản lý quyền truy cập
        public async Task<IActionResult> Trash()
        {
            return _context.TbProducts != null ?
                        View(await _context.TbProducts.Where(m => m.Status == 0).ToListAsync()) :
                        Problem("Entity set 'FiveBeachStoreContext.TbProducts'  is null.");
        }
        private bool TbProductExists(int id)
        {
          return (_context.TbProducts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
