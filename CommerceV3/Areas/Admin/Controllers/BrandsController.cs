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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CommerceV3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BrandsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment environment;

        public BrandsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            this.environment = environment;
        }

        // GET: Admin/Brands
        public async Task<IActionResult> Index()
        {
            return View(await _context.Brands.ToListAsync());
        }

        // GET: Admin/Brands/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Admin/Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Logo")] Brand brand, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                if(upload != null && upload.Length > 0)
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
                    brand.Logo = fileName;
                }
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Admin/Brands/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Logo")] Brand brand, IFormFile upload)
        {
            if (id != brand.Id)
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
                        brand.Logo = fileName;
                    }                    
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
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
            return View(brand);
        }

        // GET: Admin/Brands/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Admin/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var brand = await _context.Brands.FindAsync(id);
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(string id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }
    }
}
