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
    public class AllergensController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AllergensController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Allergens
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Allergens.Include(a => a.Ingredient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Allergens/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Allergens == null)
            {
                return NotFound();
            }

            var allergen = await _context.Allergens
                .Include(a => a.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allergen == null)
            {
                return NotFound();
            }

            return View(allergen);
        }

        // GET: Allergens/Create
        public IActionResult Create()
        {
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Description");
            return View();
        }

        // POST: Allergens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IngredientId,CreatedAt,UpdatedAt,Id")] Allergen allergen)
        {
            if (ModelState.IsValid)
            {
                allergen.Id = Guid.NewGuid();
                _context.Add(allergen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Description", allergen.IngredientId);
            return View(allergen);
        }

        // GET: Allergens/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Allergens == null)
            {
                return NotFound();
            }

            var allergen = await _context.Allergens.FindAsync(id);
            if (allergen == null)
            {
                return NotFound();
            }
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Description", allergen.IngredientId);
            return View(allergen);
        }

        // POST: Allergens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IngredientId,CreatedAt,UpdatedAt,Id")] Allergen allergen)
        {
            if (id != allergen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allergen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllergenExists(allergen.Id))
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
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Description", allergen.IngredientId);
            return View(allergen);
        }

        // GET: Allergens/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Allergens == null)
            {
                return NotFound();
            }

            var allergen = await _context.Allergens
                .Include(a => a.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allergen == null)
            {
                return NotFound();
            }

            return View(allergen);
        }

        // POST: Allergens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Allergens == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Allergens'  is null.");
            }
            var allergen = await _context.Allergens.FindAsync(id);
            if (allergen != null)
            {
                _context.Allergens.Remove(allergen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllergenExists(Guid id)
        {
          return (_context.Allergens?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
