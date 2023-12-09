using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiveBeachStore.Models;

namespace FiveBeachStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminConfigsController : Controller
    {
        private readonly FiveBeachStoreContext _context;

        public AdminConfigsController(FiveBeachStoreContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminConfigs
        public async Task<IActionResult> Index()
        {
              return _context.TbConfigs != null ? 
                          View(await _context.TbConfigs.ToListAsync()) :
                          Problem("Entity set 'FiveBeachStoreContext.TbConfigs'  is null.");
        }

        // GET: Admin/AdminConfigs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbConfigs == null)
            {
                return NotFound();
            }

            var tbConfig = await _context.TbConfigs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbConfig == null)
            {
                return NotFound();
            }

            return View(tbConfig);
        }

        // GET: Admin/AdminConfigs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminConfigs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SiteName,Metakey,Metadesc,Author,Phone,Email,Facebook,Twitter,Youtube,Googleplus,Status")] TbConfig tbConfig)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbConfig);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbConfig);
        }

        // GET: Admin/AdminConfigs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbConfigs == null)
            {
                return NotFound();
            }

            var tbConfig = await _context.TbConfigs.FindAsync(id);
            if (tbConfig == null)
            {
                return NotFound();
            }
            return View(tbConfig);
        }

        // POST: Admin/AdminConfigs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SiteName,Metakey,Metadesc,Author,Phone,Email,Facebook,Twitter,Youtube,Googleplus,Status")] TbConfig tbConfig)
        {
            if (id != tbConfig.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbConfig);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbConfigExists(tbConfig.Id))
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
            return View(tbConfig);
        }

        // GET: Admin/AdminConfigs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbConfigs == null)
            {
                return NotFound();
            }

            var tbConfig = await _context.TbConfigs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbConfig == null)
            {
                return NotFound();
            }

            return View(tbConfig);
        }

        // POST: Admin/AdminConfigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbConfigs == null)
            {
                return Problem("Entity set 'FiveBeachStoreContext.TbConfigs'  is null.");
            }
            var tbConfig = await _context.TbConfigs.FindAsync(id);
            if (tbConfig != null)
            {
                _context.TbConfigs.Remove(tbConfig);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbConfigExists(int id)
        {
          return (_context.TbConfigs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
