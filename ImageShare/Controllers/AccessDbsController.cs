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
    public class AccessDbsController : Controller
    {
        private readonly ImageShareDbContext _context;

        public AccessDbsController(ImageShareDbContext context)
        {
            _context = context;
        }

        // GET: AccessDbs
        public async Task<IActionResult> Index()
        {
            var imageShareDbContext = _context.AccessDb.Include(a => a.File).Include(a => a.User);
            return View(await imageShareDbContext.ToListAsync());
        }

        // GET: AccessDbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessDb = await _context.AccessDb
                .Include(a => a.File)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AccessId == id);
            if (accessDb == null)
            {
                return NotFound();
            }

            return View(accessDb);
        }

        // GET: AccessDbs/Create
        public IActionResult Create()
        {
            ViewData["FileId"] = new SelectList(_context.FileDbs, "FileId", "FileId");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: AccessDbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccessId,UserId,FileId")] AccessDb accessDb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accessDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FileId"] = new SelectList(_context.FileDbs, "FileId", "FileId", accessDb.FileId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", accessDb.UserId);
            return View(accessDb);
        }

        // GET: AccessDbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessDb = await _context.AccessDb.FindAsync(id);
            if (accessDb == null)
            {
                return NotFound();
            }
            ViewData["FileId"] = new SelectList(_context.FileDbs, "FileId", "FileId", accessDb.FileId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", accessDb.UserId);
            return View(accessDb);
        }

        // POST: AccessDbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccessId,UserId,FileId")] AccessDb accessDb)
        {
            if (id != accessDb.AccessId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accessDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccessDbExists(accessDb.AccessId))
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
            ViewData["FileId"] = new SelectList(_context.FileDbs, "FileId", "FileId", accessDb.FileId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", accessDb.UserId);
            return View(accessDb);
        }

        // GET: AccessDbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessDb = await _context.AccessDb
                .Include(a => a.File)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AccessId == id);
            if (accessDb == null)
            {
                return NotFound();
            }

            return View(accessDb);
        }

        // POST: AccessDbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accessDb = await _context.AccessDb.FindAsync(id);
            _context.AccessDb.Remove(accessDb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccessDbExists(int id)
        {
            return _context.AccessDb.Any(e => e.AccessId == id);
        }
    }
}
