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
    public class MenuItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MenuItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MenuItems.Include(m => m.ParentMenuItem);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/MenuItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .Include(m => m.ParentMenuItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // GET: Admin/MenuItems/Create
        public IActionResult Create()
        {
            ViewData["ParentMenuItemId"] = new SelectList(_context.MenuItems, "Id", "Name");
            return View();
        }

        // POST: Admin/MenuItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Url,Target,IsPublished,Position,ParentMenuItemId")] MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentMenuItemId"] = new SelectList(_context.MenuItems, "Id", "Name", menuItem.ParentMenuItemId);
            return View(menuItem);
        }

        // GET: Admin/MenuItems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            ViewData["ParentMenuItemId"] = new SelectList(_context.MenuItems, "Id", "Name", menuItem.ParentMenuItemId);
            return View(menuItem);
        }

        // POST: Admin/MenuItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Url,Target,IsPublished,Position,ParentMenuItemId")] MenuItem menuItem)
        {
            if (id != menuItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemExists(menuItem.Id))
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
            ViewData["ParentMenuItemId"] = new SelectList(_context.MenuItems, "Id", "Name", menuItem.ParentMenuItemId);
            return View(menuItem);
        }

        // GET: Admin/MenuItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems
                .Include(m => m.ParentMenuItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // POST: Admin/MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuItemExists(string id)
        {
            return _context.MenuItems.Any(e => e.Id == id);
        }
    }
}
