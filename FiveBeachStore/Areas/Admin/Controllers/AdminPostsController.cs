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
    public class AdminPostsController : Controller
    {
        private readonly FiveBeachStoreContext _context;
        public INotyfService _notifyServive { get; }
        public AdminPostsController(FiveBeachStoreContext context, INotyfService notifyServive)
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
            var lsPosts = _context.TbPosts
                .AsNoTracking()
                .Where(m => m.Status != 0)
                .OrderByDescending(x => x.Id);
            PagedList<TbPost> models = new PagedList<TbPost>(lsPosts, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);

        }

        // GET: Admin/AdminPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbPosts == null)
            {
                return NotFound();
            }

            var tbPost = await _context.TbPosts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbPost == null)
            {
                return NotFound();
            }

            return View(tbPost);
        }

        // GET: Admin/AdminPosts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TopicId,Title,Slug,Detail,Image,Type,Metakey,Metadesc,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbPost tbPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbPost);
        }

        // GET: Admin/AdminPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbPosts == null)
            {
                return NotFound();
            }

            var tbPost = await _context.TbPosts.FindAsync(id);
            if (tbPost == null)
            {
                return NotFound();
            }
            return View(tbPost);
        }

        // POST: Admin/AdminPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TopicId,Title,Slug,Detail,Image,Type,Metakey,Metadesc,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Status")] TbPost tbPost)
        {
            if (id != tbPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbPostExists(tbPost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tbPost);
        }

        // GET: Admin/AdminPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbPosts == null)
            {
                return NotFound();
            }

            var tbPost = await _context.TbPosts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbPost == null)
            {
                return NotFound();
            }

            return View(tbPost);
        }

        // POST: Admin/AdminPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbPosts == null)
            {
                return Problem("Entity set 'FiveBeachStoreContext.TbPosts'  is null.");
            }
            var tbPost = await _context.TbPosts.FindAsync(id);
            if (tbPost != null)
            {
                _context.TbPosts.Remove(tbPost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbPostExists(int id)
        {
          return (_context.TbPosts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
