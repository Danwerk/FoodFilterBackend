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
    public class FoodAllergensController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodAllergensController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FoodAllergens
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FoodAllergens.Include(f => f.Allergen).Include(f => f.Food);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FoodAllergens/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.FoodAllergens == null)
            {
                return NotFound();
            }

            var foodAllergen = await _context.FoodAllergens
                .Include(f => f.Allergen)
                .Include(f => f.Food)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodAllergen == null)
            {
                return NotFound();
            }

            return View(foodAllergen);
        }

        // GET: FoodAllergens/Create
        public IActionResult Create()
        {
            ViewData["AllergenId"] = new SelectList(_context.Allergens, "Id", "Id");
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Description");
            return View();
        }

        // POST: FoodAllergens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodId,AllergenId,CreatedAt,UpdatedAt,Id")] FoodAllergen foodAllergen)
        {
            if (ModelState.IsValid)
            {
                foodAllergen.Id = Guid.NewGuid();
                _context.Add(foodAllergen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AllergenId"] = new SelectList(_context.Allergens, "Id", "Id", foodAllergen.AllergenId);
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Description", foodAllergen.FoodId);
            return View(foodAllergen);
        }

        // GET: FoodAllergens/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.FoodAllergens == null)
            {
                return NotFound();
            }

            var foodAllergen = await _context.FoodAllergens.FindAsync(id);
            if (foodAllergen == null)
            {
                return NotFound();
            }
            ViewData["AllergenId"] = new SelectList(_context.Allergens, "Id", "Id", foodAllergen.AllergenId);
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Description", foodAllergen.FoodId);
            return View(foodAllergen);
        }

        // POST: FoodAllergens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FoodId,AllergenId,CreatedAt,UpdatedAt,Id")] FoodAllergen foodAllergen)
        {
            if (id != foodAllergen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodAllergen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodAllergenExists(foodAllergen.Id))
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
            ViewData["AllergenId"] = new SelectList(_context.Allergens, "Id", "Id", foodAllergen.AllergenId);
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Description", foodAllergen.FoodId);
            return View(foodAllergen);
        }

        // GET: FoodAllergens/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.FoodAllergens == null)
            {
                return NotFound();
            }

            var foodAllergen = await _context.FoodAllergens
                .Include(f => f.Allergen)
                .Include(f => f.Food)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodAllergen == null)
            {
                return NotFound();
            }

            return View(foodAllergen);
        }

        // POST: FoodAllergens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.FoodAllergens == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FoodAllergens'  is null.");
            }
            var foodAllergen = await _context.FoodAllergens.FindAsync(id);
            if (foodAllergen != null)
            {
                _context.FoodAllergens.Remove(foodAllergen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodAllergenExists(Guid id)
        {
          return (_context.FoodAllergens?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
