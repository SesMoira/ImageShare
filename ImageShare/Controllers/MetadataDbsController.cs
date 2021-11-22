using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ImageShare.Data;
using ImageShare.Models;

namespace ImageShare.Controllers
{
    public class MetadataDbsController : Controller
    {
        private readonly ImageShareDbContext _context;

        public MetadataDbsController(ImageShareDbContext context)
        {
            _context = context;
        }

        // GET: MetadataDbs
        public async Task<IActionResult> Index()
        {
            var imageShareDbContext = _context.MetadataDb.Include(m => m.File);
            return View(await imageShareDbContext.ToListAsync());
        }

        // GET: MetadataDbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metadataDb = await _context.MetadataDb
                .Include(m => m.File)
                .FirstOrDefaultAsync(m => m.MetadataId == id);
            if (metadataDb == null)
            {
                return NotFound();
            }

            return View(metadataDb);
        }

        // GET: MetadataDbs/Create
        public IActionResult Create()
        {
            ViewData["FileId"] = new SelectList(_context.FileDbs, "FileId", "FileId");
            return View();
        }

        // POST: MetadataDbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MetadataId,FileId,FileTitle,CaptureBy,CapturedDate,Tags")] MetadataDb metadataDb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metadataDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FileId"] = new SelectList(_context.FileDbs, "FileId", "FileId", metadataDb.FileId);
            return View(metadataDb);
        }

        // GET: MetadataDbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metadataDb = await _context.MetadataDb.FindAsync(id);
            if (metadataDb == null)
            {
                return NotFound();
            }
            ViewData["FileId"] = new SelectList(_context.FileDbs, "FileId", "FileId", metadataDb.FileId);
            return View(metadataDb);
        }

        // POST: MetadataDbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MetadataId,FileId,FileTitle,CaptureBy,CapturedDate,Tags")] MetadataDb metadataDb)
        {
            if (id != metadataDb.MetadataId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metadataDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetadataDbExists(metadataDb.MetadataId))
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
            ViewData["FileId"] = new SelectList(_context.FileDbs, "FileId", "FileId", metadataDb.FileId);
            return View(metadataDb);
        }

        // GET: MetadataDbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metadataDb = await _context.MetadataDb
                .Include(m => m.File)
                .FirstOrDefaultAsync(m => m.MetadataId == id);
            if (metadataDb == null)
            {
                return NotFound();
            }

            return View(metadataDb);
        }

        // POST: MetadataDbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metadataDb = await _context.MetadataDb.FindAsync(id);
            _context.MetadataDb.Remove(metadataDb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MetadataDbExists(int id)
        {
            return _context.MetadataDb.Any(e => e.MetadataId == id);
        }
    }
}
