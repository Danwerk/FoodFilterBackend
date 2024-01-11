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
    public class FoodNutrientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodNutrientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FoodNutrients
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FoodNutrients.Include(f => f.Food).Include(f => f.Nutrient).Include(f => f.Unit);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FoodNutrients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.FoodNutrients == null)
            {
                return NotFound();
            }

            var foodNutrient = await _context.FoodNutrients
                .Include(f => f.Food)
                .Include(f => f.Nutrient)
                .Include(f => f.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodNutrient == null)
            {
                return NotFound();
            }

            return View(foodNutrient);
        }

        // GET: FoodNutrients/Create
        public IActionResult Create()
        {
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Description");
            ViewData["NutrientId"] = new SelectList(_context.Nutrients, "Id", "Name");
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "UnitName");
            return View();
        }

        // POST: FoodNutrients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnitId,FoodId,NutrientId,Amount,CreatedAt,UpdatedAt,Id")] FoodNutrient foodNutrient)
        {
            if (ModelState.IsValid)
            {
                foodNutrient.Id = Guid.NewGuid();
                _context.Add(foodNutrient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Description", foodNutrient.FoodId);
            ViewData["NutrientId"] = new SelectList(_context.Nutrients, "Id", "Name", foodNutrient.NutrientId);
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "UnitName", foodNutrient.UnitId);
            return View(foodNutrient);
        }

        // GET: FoodNutrients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.FoodNutrients == null)
            {
                return NotFound();
            }

            var foodNutrient = await _context.FoodNutrients.FindAsync(id);
            if (foodNutrient == null)
            {
                return NotFound();
            }
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Description", foodNutrient.FoodId);
            ViewData["NutrientId"] = new SelectList(_context.Nutrients, "Id", "Name", foodNutrient.NutrientId);
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "UnitName", foodNutrient.UnitId);
            return View(foodNutrient);
        }

        // POST: FoodNutrients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UnitId,FoodId,NutrientId,Amount,CreatedAt,UpdatedAt,Id")] FoodNutrient foodNutrient)
        {
            if (id != foodNutrient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodNutrient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodNutrientExists(foodNutrient.Id))
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
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Description", foodNutrient.FoodId);
            ViewData["NutrientId"] = new SelectList(_context.Nutrients, "Id", "Name", foodNutrient.NutrientId);
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "UnitName", foodNutrient.UnitId);
            return View(foodNutrient);
        }

        // GET: FoodNutrients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.FoodNutrients == null)
            {
                return NotFound();
            }

            var foodNutrient = await _context.FoodNutrients
                .Include(f => f.Food)
                .Include(f => f.Nutrient)
                .Include(f => f.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodNutrient == null)
            {
                return NotFound();
            }

            return View(foodNutrient);
        }

        // POST: FoodNutrients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.FoodNutrients == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FoodNutrients'  is null.");
            }
            var foodNutrient = await _context.FoodNutrients.FindAsync(id);
            if (foodNutrient != null)
            {
                _context.FoodNutrients.Remove(foodNutrient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodNutrientExists(Guid id)
        {
          return (_context.FoodNutrients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
