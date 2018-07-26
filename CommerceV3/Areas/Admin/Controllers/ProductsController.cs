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
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment environment;

        public ProductsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            this.environment = environment;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Brand).Include(p => p.Supplier);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            var product = new Product();
            product.CreateDate = DateTime.Now;
            product.CreatedBy = User.Identity.Name;
            product.UpdateDate = DateTime.Now;
            product.UpdatedBy = User.Identity.Name;

            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name");
            return View(product);
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Slug,Description,OldPrice,Price,Quantity,IsInStock,IsPublished,IsNew,ShippingPriceCityWide,ShippingPriceCountryWide,ShippingPriceWorldWide,SupplierId,BrandId,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Photo")] Product product, IFormFile upload)
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
                    product.Photo = fileName;
                }
                product.CreateDate = DateTime.Now;
                product.CreatedBy = User.Identity.Name;
                product.UpdateDate = DateTime.Now;
                product.UpdatedBy = User.Identity.Name;

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Slug,Description,OldPrice,Price,Quantity,IsInStock,IsPublished,IsNew,ShippingPriceCityWide,ShippingPriceCountryWide,ShippingPriceWorldWide,SupplierId,BrandId,CreatedBy,CreateDate,UpdatedBy,UpdateDate,Photo")] Product product, IFormFile upload)
        {
            if (id != product.Id)
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
                        product.Photo = fileName;
                    }
                    product.UpdateDate = DateTime.Now;
                    product.UpdatedBy = User.Identity.Name;
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
