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
    public class FoodIngredientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodIngredientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FoodIngredients
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FoodIngredients.Include(f => f.Food).Include(f => f.Ingredient).Include(f => f.Unit);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FoodIngredients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.FoodIngredients == null)
            {
                return NotFound();
            }

            var foodIngredient = await _context.FoodIngredients
                .Include(f => f.Food)
                .Include(f => f.Ingredient)
                .Include(f => f.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodIngredient == null)
            {
                return NotFound();
            }

            return View(foodIngredient);
        }

        // GET: FoodIngredients/Create
        public IActionResult Create()
        {
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Description");
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Description");
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "UnitName");
            return View();
        }

        // POST: FoodIngredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnitId,FoodId,IngredientId,Amount,CreatedAt,UpdatedAt,Id")] FoodIngredient foodIngredient)
        {
            if (ModelState.IsValid)
            {
                foodIngredient.Id = Guid.NewGuid();
                _context.Add(foodIngredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Description", foodIngredient.FoodId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Description", foodIngredient.IngredientId);
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "UnitName", foodIngredient.UnitId);
            return View(foodIngredient);
        }

        // GET: FoodIngredients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.FoodIngredients == null)
            {
                return NotFound();
            }

            var foodIngredient = await _context.FoodIngredients.FindAsync(id);
            if (foodIngredient == null)
            {
                return NotFound();
            }
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Description", foodIngredient.FoodId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Description", foodIngredient.IngredientId);
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "UnitName", foodIngredient.UnitId);
            return View(foodIngredient);
        }

        // POST: FoodIngredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UnitId,FoodId,IngredientId,Amount,CreatedAt,UpdatedAt,Id")] FoodIngredient foodIngredient)
        {
            if (id != foodIngredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodIngredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodIngredientExists(foodIngredient.Id))
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
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Description", foodIngredient.FoodId);
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Description", foodIngredient.IngredientId);
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "UnitName", foodIngredient.UnitId);
            return View(foodIngredient);
        }

        // GET: FoodIngredients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.FoodIngredients == null)
            {
                return NotFound();
            }

            var foodIngredient = await _context.FoodIngredients
                .Include(f => f.Food)
                .Include(f => f.Ingredient)
                .Include(f => f.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodIngredient == null)
            {
                return NotFound();
            }

            return View(foodIngredient);
        }

        // POST: FoodIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.FoodIngredients == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FoodIngredients'  is null.");
            }
            var foodIngredient = await _context.FoodIngredients.FindAsync(id);
            if (foodIngredient != null)
            {
                _context.FoodIngredients.Remove(foodIngredient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodIngredientExists(Guid id)
        {
          return (_context.FoodIngredients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
