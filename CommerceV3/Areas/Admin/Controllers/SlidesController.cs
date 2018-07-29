using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommerceV3.Data;
using CommerceV3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CommerceV3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SlidesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment environment;

        public SlidesController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            this.environment = environment;
        }

        // GET: Admin/Slides
        public async Task<IActionResult> Index()
        {
            return View(await _context.Slides.ToListAsync());
        }

        // GET: Admin/Slides/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slide = await _context.Slides
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slide == null)
            {
                return NotFound();
            }

            return View(slide);
        }

        // GET: Admin/Slides/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Slides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Image,Url,Target,IsPublished,Position")] Slide slide, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.Length > 0)
                {
                    //upload işlemi burada yapılır.
                    var rnd = new Random();
                    var fileName = Path.GetFileNameWithoutExtension(upload.FileName) + rnd.Next(1000).ToString() + Path.GetExtension(upload.FileName);
                    var path = Path.Combine(environment.WebRootPath, "Uploads");
                    var filePath = Path.Combine(path, fileName);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        upload.CopyTo(stream);
                    }
                    slide.Image = fileName;
                }
                _context.Add(slide);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slide);
        }

        // GET: Admin/Slides/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slide = await _context.Slides.FindAsync(id);
            if (slide == null)
            {
                return NotFound();
            }
            return View(slide);
        }

        // POST: Admin/Slides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Image,Url,Target,IsPublished,Position")] Slide slide, IFormFile upload)
        {
            if (id != slide.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {               
                try
                {
                    if (upload != null && upload.Length > 0)
                    {
                        //upload işlemi burada yapılır.
                        var rnd = new Random();
                        var fileName = Path.GetFileNameWithoutExtension(upload.FileName) + rnd.Next(1000).ToString() + Path.GetExtension(upload.FileName);
                        var path = Path.Combine(environment.WebRootPath, "Uploads");
                        var filePath = Path.Combine(path, fileName);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            upload.CopyTo(stream);
                        }
                        slide.Image = fileName;
                    }
                    _context.Update(slide);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SlideExists(slide.Id))
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
            return View(slide);
        }

        // GET: Admin/Slides/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slide = await _context.Slides
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slide == null)
            {
                return NotFound();
            }

            return View(slide);
        }

        // POST: Admin/Slides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var slide = await _context.Slides.FindAsync(id);
            _context.Slides.Remove(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SlideExists(string id)
        {
            return _context.Slides.Any(e => e.Id == id);
        }
    }
}
