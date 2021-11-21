using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ImageShare.Data;
using ImageShare.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ImageShare.Controllers
{
    public class FileDbsController : Controller
    {
        private readonly ImageShareDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileDbsController(ImageShareDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        // GET: FileDbs
        public async Task<IActionResult> Index()
        {
            return View(await _context.FileDb.ToListAsync());
        }

        // GET: FileDbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileDb = await _context.FileDb
                .FirstOrDefaultAsync(m => m.FileId == id);
            if (fileDb == null)
            {
                return NotFound();
            }

            return View(fileDb);
        }

        // GET: FileDbs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FileDbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FileId,FileTitle,FileCapturedBy,FileCapturedDate,FileGeolocation,FileUrl,FileTags,ImageFile")] FileDb fileDb)
        {
            if (ModelState.IsValid)
            {
                //Saving image to wwwroot/Image
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(fileDb.ImageFile.FileName);
                string extension = Path.GetExtension(fileDb.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", filename);
                using (var fileStream = new FileStream(path,FileMode.Create))
                {
                    await fileDb.ImageFile.CopyToAsync(fileStream);
                }
                //Inserting records
                _context.Add(fileDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fileDb);
        }

        // GET: FileDbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileDb = await _context.FileDb.FindAsync(id);
            if (fileDb == null)
            {
                return NotFound();
            }
            return View(fileDb);
        }

        // POST: FileDbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FileId,FileTitle,FileCapturedBy,FileCapturedDate,FileGeolocation,FileUrl,FileTags,ImageFile")] FileDb fileDb)
        {
            if (id != fileDb.FileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileDbExists(fileDb.FileId))
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
            return View(fileDb);
        }

        // GET: FileDbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileDb = await _context.FileDb
                .FirstOrDefaultAsync(m => m.FileId == id);
            if (fileDb == null)
            {
                return NotFound();
            }

            return View(fileDb);
        }

        // POST: FileDbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var fileDb = await _context.FileDb.FindAsync(id);

            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "image", fileDb.FileTitle);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            //Delete record
            _context.FileDb.Remove(fileDb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileDbExists(int id)
        {
            return _context.FileDb.Any(e => e.FileId == id);
        }
    }
}
