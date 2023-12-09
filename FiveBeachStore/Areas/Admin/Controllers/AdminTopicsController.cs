using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiveBeachStore.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using PagedList.Core;

namespace FiveBeachStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminTopicsController : Controller
    {
        private readonly FiveBeachStoreContext _context;
        public INotyfService _notifyServive { get; }
        public AdminTopicsController(FiveBeachStoreContext context, INotyfService notifyServive)
        {
            _context = context;
            _notifyServive = notifyServive;
        }



        // GET: Admin/AdminPages


        public IActionResult Index(int? page)
        {
            //ViewData["QuyenTruyCap"] = new SelectList(_context.TbPosts, "Id", "Metadesc");
            //List<SelectListItem> lsTrangThai = new List<SelectListItem>();
            //lsTrangThai.Add(new SelectListItem() { Text = "On", Value = "1" });
            //lsTrangThai.Add(new SelectListItem() { Text = "Off", Value = "2" });
            //ViewData["lsTrangThai"] = lsTrangThai;
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 1;
            var lsTopics = _context.TbTopics
                .AsNoTracking()
                .Where(m => m.Status != 0)
                .OrderByDescending(x => x.Id);
            PagedList<TbTopic> models = new PagedList<TbTopic>(lsTopics, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);

        }

        // GET: Admin/AdminTopics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbTopics == null)
            {
                return NotFound();
            }

            var tbTopic = await _context.TbTopics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbTopic == null)
            {
                return NotFound();
            }

            return View(tbTopic);
        }

        // GET: Admin/AdminTopics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminTopics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Slug,ParentId,SortOrder,Metakey,Metadesc,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbTopic tbTopic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbTopic);
                await _context.SaveChangesAsync();
                _notifyServive.Success("Thêm chủ đề thành công!");
                return RedirectToAction(nameof(Index));
            }
            return View(tbTopic);
        }

        // GET: Admin/AdminTopics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbTopics == null)
            {
                return NotFound();
            }

            var tbTopic = await _context.TbTopics.FindAsync(id);
            if (tbTopic == null)
            {
                return NotFound();
            }
            return View(tbTopic);
        }

        // POST: Admin/AdminTopics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Slug,ParentId,SortOrder,Metakey,Metadesc,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbTopic tbTopic)
        {
            if (id != tbTopic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbTopic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbTopicExists(tbTopic.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _notifyServive.Success("Chỉnh sửa chủ đề thành công!");
                return RedirectToAction(nameof(Index));
            }
            return View(tbTopic);
        }

        // GET: Admin/AdminTopics/Destroy/5
        public async Task<IActionResult> Destroy(int? id)
        {
            if (id == null || _context.TbTopics == null)
            {
                return NotFound();
            }

            var tbTopic = await _context.TbTopics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbTopic == null)
            {
                return NotFound();
            }

            return View(tbTopic);
        }

        // POST: Admin/AdminTopics/Destroy/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DestroyConfirmed(int id)
        {
            if (_context.TbTopics == null)
            {
                return Problem("Entity set 'FiveBeachStoreContext.TbTopics'  is null.");
            }
            var tbTopic = await _context.TbTopics.FindAsync(id);
            if (tbTopic != null)
            {
                _context.TbTopics.Remove(tbTopic);
            }
            
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa chủ đề thành công!");
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var tbtopics = await _context.TbBrands.FindAsync(id);
            tbtopics.Status = 0;
            _context.Update(tbtopics);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Xóa chủ đề vào thùng rác thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Status/5
        // Thay đổi trạng thái Status
        public async Task<IActionResult> Status(int? id)
        {
            var tbtopics = await _context.TbBrands.FindAsync(id);
            int v = (tbtopics.Status == 2) ? 1 : 2;
            tbtopics.Status = (byte?)v;
            tbtopics.UpdatedAt = DateTime.Now;
            tbtopics.UpdatedBy = 1;
            _context.Update(tbtopics);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Thay đổi trạng thái chủ đề thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Restore/5
        //Khôi phục Status==2
        public async Task<IActionResult> Restore(int? id)
        {
            var tbtopics = await _context.TbBrands.FindAsync(id);
            tbtopics.Status = 2;
            _context.Update(tbtopics);
            await _context.SaveChangesAsync();
            _notifyServive.Success("Hoàn tác chủ đề thành công!");
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminRole/Trash/5
        //Hiện danh sách của quản lý quyền truy cập
        public async Task<IActionResult> Trash()
        {
            return _context.TbTopics != null ?
                        View(await _context.TbTopics.Where(m => m.Status == 0).ToListAsync()) :
                        Problem("Entity set 'FiveBeachStoreContext.TbTopics'  is null.");
        }
        private bool TbTopicExists(int id)
        {
          return (_context.TbTopics?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
