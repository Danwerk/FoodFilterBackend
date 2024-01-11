using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain;
using DAL.EF;

namespace WebApp.Areas_Admin_Controllers
{
    public class NutrientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NutrientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Nutrients
        public async Task<IActionResult> Index()
        {
              return _context.Nutrients != null ? 
                          View(await _context.Nutrients.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Nutrients'  is null.");
        }

        // GET: Nutrients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Nutrients == null)
            {
                return NotFound();
            }

            var nutrient = await _context.Nutrients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nutrient == null)
            {
                return NotFound();
            }

            return View(nutrient);
        }

        // GET: Nutrients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nutrients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CreatedAt,UpdatedAt,Id")] Nutrient nutrient)
        {
            if (ModelState.IsValid)
            {
                nutrient.Id = Guid.NewGuid();
                _context.Add(nutrient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nutrient);
        }

        // GET: Nutrients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Nutrients == null)
            {
                return NotFound();
            }

            var nutrient = await _context.Nutrients.FindAsync(id);
            if (nutrient == null)
            {
                return NotFound();
            }
            return View(nutrient);
        }

        // POST: Nutrients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,CreatedAt,UpdatedAt,Id")] Nutrient nutrient)
        {
            if (id != nutrient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nutrient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NutrientExists(nutrient.Id))
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
            return View(nutrient);
        }

        // GET: Nutrients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Nutrients == null)
            {
                return NotFound();
            }

            var nutrient = await _context.Nutrients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nutrient == null)
            {
                return NotFound();
            }

            return View(nutrient);
        }

        // POST: Nutrients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Nutrients == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Nutrients'  is null.");
            }
            var nutrient = await _context.Nutrients.FindAsync(id);
            if (nutrient != null)
            {
                _context.Nutrients.Remove(nutrient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NutrientExists(Guid id)
        {
          return (_context.Nutrients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
