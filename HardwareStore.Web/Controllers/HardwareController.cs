using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HardwareStore.Infrastructure;
using HardwareStore.Infrastructure.Models;

namespace HardwareStore.Web.Controllers
{
    public class HardwareController : Controller
    {
        private readonly HardwareContext _context;

        public HardwareController(HardwareContext context)
        {
            _context = context;
        }

        // GET: Hardware
        public async Task<IActionResult> Index()
        {
            var hardwareContext = _context.HardwareItems.Include(h => h.Brand);
            return View(await hardwareContext.ToListAsync());
        }

        // GET: Hardware/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hardwareModel = await _context.HardwareItems
                .Include(h => h.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hardwareModel == null)
            {
                return NotFound();
            }

            return View(hardwareModel);
        }

        // GET: Hardware/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Country");
            return View();
        }

        // POST: Hardware/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ModelName,Price,BrandId")] HardwareModel hardwareModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hardwareModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Country", hardwareModel.BrandId);
            return View(hardwareModel);
        }

        // GET: Hardware/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hardwareModel = await _context.HardwareItems.FindAsync(id);
            if (hardwareModel == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Country", hardwareModel.BrandId);
            return View(hardwareModel);
        }

        // POST: Hardware/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ModelName,Price,BrandId")] HardwareModel hardwareModel)
        {
            if (id != hardwareModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hardwareModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HardwareModelExists(hardwareModel.Id))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Country", hardwareModel.BrandId);
            return View(hardwareModel);
        }

        // GET: Hardware/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hardwareModel = await _context.HardwareItems
                .Include(h => h.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hardwareModel == null)
            {
                return NotFound();
            }

            return View(hardwareModel);
        }

        // POST: Hardware/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hardwareModel = await _context.HardwareItems.FindAsync(id);
            if (hardwareModel != null)
            {
                _context.HardwareItems.Remove(hardwareModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HardwareModelExists(int id)
        {
            return _context.HardwareItems.Any(e => e.Id == id);
        }
    }
}
