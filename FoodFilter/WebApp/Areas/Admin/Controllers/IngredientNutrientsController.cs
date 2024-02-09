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
    public class IngredientNutrientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IngredientNutrientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IngredientNutrients
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IngredientNutrient.Include(i => i.Ingredient).Include(i => i.Nutrient).Include(i => i.Unit);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: IngredientNutrients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.IngredientNutrient == null)
            {
                return NotFound();
            }

            var ingredientNutrient = await _context.IngredientNutrient
                .Include(i => i.Ingredient)
                .Include(i => i.Nutrient)
                .Include(i => i.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredientNutrient == null)
            {
                return NotFound();
            }

            return View(ingredientNutrient);
        }

        // GET: IngredientNutrients/Create
        public IActionResult Create()
        {
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Description");
            ViewData["NutrientId"] = new SelectList(_context.Nutrients, "Id", "Name");
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "UnitName");
            return View();
        }

        // POST: IngredientNutrients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnitId,IngredientId,NutrientId,Amount,CreatedAt,UpdatedAt,Id")] IngredientNutrient ingredientNutrient)
        {
            if (ModelState.IsValid)
            {
                ingredientNutrient.Id = Guid.NewGuid();
                _context.Add(ingredientNutrient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Description", ingredientNutrient.IngredientId);
            ViewData["NutrientId"] = new SelectList(_context.Nutrients, "Id", "Name", ingredientNutrient.NutrientId);
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "UnitName", ingredientNutrient.UnitId);
            return View(ingredientNutrient);
        }

        // GET: IngredientNutrients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.IngredientNutrient == null)
            {
                return NotFound();
            }

            var ingredientNutrient = await _context.IngredientNutrient.FindAsync(id);
            if (ingredientNutrient == null)
            {
                return NotFound();
            }
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Description", ingredientNutrient.IngredientId);
            ViewData["NutrientId"] = new SelectList(_context.Nutrients, "Id", "Name", ingredientNutrient.NutrientId);
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "UnitName", ingredientNutrient.UnitId);
            return View(ingredientNutrient);
        }

        // POST: IngredientNutrients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UnitId,IngredientId,NutrientId,Amount,CreatedAt,UpdatedAt,Id")] IngredientNutrient ingredientNutrient)
        {
            if (id != ingredientNutrient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredientNutrient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientNutrientExists(ingredientNutrient.Id))
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
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Description", ingredientNutrient.IngredientId);
            ViewData["NutrientId"] = new SelectList(_context.Nutrients, "Id", "Name", ingredientNutrient.NutrientId);
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "UnitName", ingredientNutrient.UnitId);
            return View(ingredientNutrient);
        }

        // GET: IngredientNutrients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.IngredientNutrient == null)
            {
                return NotFound();
            }

            var ingredientNutrient = await _context.IngredientNutrient
                .Include(i => i.Ingredient)
                .Include(i => i.Nutrient)
                .Include(i => i.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredientNutrient == null)
            {
                return NotFound();
            }

            return View(ingredientNutrient);
        }

        // POST: IngredientNutrients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.IngredientNutrient == null)
            {
                return Problem("Entity set 'ApplicationDbContext.IngredientNutrient'  is null.");
            }
            var ingredientNutrient = await _context.IngredientNutrient.FindAsync(id);
            if (ingredientNutrient != null)
            {
                _context.IngredientNutrient.Remove(ingredientNutrient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientNutrientExists(Guid id)
        {
          return (_context.IngredientNutrient?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
