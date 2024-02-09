using App.Contracts.DAL;
using App.Domain;
using DAL.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
#pragma warning disable 1591

namespace WebApp.Areas.Admin.Controllers
{
    public class FoodNutrientsController : Controller
    {
        private readonly IAppUOW _uow;

        public FoodNutrientsController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: FoodNutrients
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.FoodNutrientRepository.AllAsync();
            return View(vm);
        }

        // GET: FoodNutrients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodNutrient = await _uow.FoodNutrientRepository.FindAsync(id.Value);
               
            if (foodNutrient == null)
            {
                return NotFound();
            }

            return View(foodNutrient);
        }

        // GET: FoodNutrients/Create
        public IActionResult Create()
        {
            ViewData["FoodId"] = new SelectList(_uow.FoodRepository.AllAsync().Result, "Id", "Description");
            ViewData["NutrientId"] = new SelectList(_uow.NutrientRepository.AllAsync().Result, "Id", "Name");
            ViewData["UnitId"] = new SelectList(_uow.UnitRepository.AllAsync().Result, "Id", "UnitName");
            return View();
        }

        // POST: FoodNutrients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodNutrient foodNutrient)
        {
            if (ModelState.IsValid)
            {
                foodNutrient.Id = Guid.NewGuid();
                _uow.FoodNutrientRepository.Add(foodNutrient);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodId"] = new SelectList(_uow.FoodRepository.AllAsync().Result, "Id", "Description", foodNutrient.FoodId);
            ViewData["NutrientId"] = new SelectList(_uow.NutrientRepository.AllAsync().Result, "Id", "Name", foodNutrient.NutrientId);
            ViewData["UnitId"] = new SelectList(_uow.UnitRepository.AllAsync().Result, "Id", "UnitName", foodNutrient.UnitId);
            return View(foodNutrient);
        }

        // GET: FoodNutrients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodNutrient = await _uow.FoodNutrientRepository.FindAsync(id.Value);
            if (foodNutrient == null)
            {
                return NotFound();
            }
            ViewData["FoodId"] = new SelectList(_uow.FoodRepository.AllAsync().Result, "Id", "Description", foodNutrient.FoodId);
            ViewData["NutrientId"] = new SelectList(_uow.NutrientRepository.AllAsync().Result, "Id", "Name", foodNutrient.NutrientId);
            ViewData["UnitId"] = new SelectList(_uow.UnitRepository.AllAsync().Result, "Id", "UnitName", foodNutrient.UnitId);
            return View(foodNutrient);
        }

        // POST: FoodNutrients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FoodNutrient foodNutrient)
        {
            if (id != foodNutrient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.FoodNutrientRepository.Update(foodNutrient);
                    await _uow.SaveChangesAsync();
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
            ViewData["FoodId"] = new SelectList(_uow.FoodRepository.AllAsync().Result, "Id", "Description", foodNutrient.FoodId);
            ViewData["NutrientId"] = new SelectList(_uow.NutrientRepository.AllAsync().Result, "Id", "Name", foodNutrient.NutrientId);
            ViewData["UnitId"] = new SelectList(_uow.UnitRepository.AllAsync().Result, "Id", "UnitName", foodNutrient.UnitId);
            return View(foodNutrient);
        }

        // GET: FoodNutrients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodNutrient = await _uow.FoodNutrientRepository.FindAsync(id.Value);
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
            var foodNutrient = await _uow.FoodNutrientRepository.FindAsync(id);
            if (foodNutrient != null)
            {
                _uow.FoodNutrientRepository.Remove(foodNutrient);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodNutrientExists(Guid id)
        {
          return (_uow.FoodNutrientRepository.AllAsync().Result?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
