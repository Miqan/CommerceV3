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

namespace CommerceV3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SuppliersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuppliersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Suppliers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Suppliers.Include(s => s.Region);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Suppliers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .Include(s => s.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Admin/Suppliers/Create
        public IActionResult Create()
        {
            var supplier = new Supplier();
            supplier.CreateDate = DateTime.Now;
            supplier.CreatedBy = User.Identity.Name;
            supplier.UpdateDate = DateTime.Now;
            supplier.UpdatedBy = User.Identity.Name;
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Name");
            return View(supplier);
        }

        // POST: Admin/Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,RegionId,CreatedBy,CreateDate,UpdatedBy,UpdateDate")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                supplier.CreateDate = DateTime.Now;
                supplier.CreatedBy = User.Identity.Name;
                supplier.UpdateDate = DateTime.Now;
                supplier.UpdatedBy = User.Identity.Name;
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Name", supplier.RegionId);
            return View(supplier);
        }

        // GET: Admin/Suppliers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Name", supplier.RegionId);
            return View(supplier);
        }

        // POST: Admin/Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,RegionId,CreatedBy,CreateDate,UpdatedBy,UpdateDate")] Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    supplier.UpdateDate = DateTime.Now;
                    supplier.UpdatedBy = User.Identity.Name;
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.Id))
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
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Name", supplier.RegionId);
            return View(supplier);
        }

        // GET: Admin/Suppliers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .Include(s => s.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Admin/Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(string id)
        {
            return _context.Suppliers.Any(e => e.Id == id);
        }
    }
}
