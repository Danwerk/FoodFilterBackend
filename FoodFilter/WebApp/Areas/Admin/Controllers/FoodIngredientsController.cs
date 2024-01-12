using App.Contracts.DAL;
using App.Domain;
using DAL.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    public class FoodIngredientsController : Controller
    {
        private readonly IAppUOW _uow;

        public FoodIngredientsController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: FoodIngredients
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.FoodIngredientRepository.AllAsync();
            return View(vm);
        }

        // GET: FoodIngredients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodIngredient = await _uow.FoodIngredientRepository.FindAsync(id.Value);
            if (foodIngredient == null)
            {
                return NotFound();
            }

            return View(foodIngredient);
        }

        // GET: FoodIngredients/Create
        public IActionResult Create()
        {
            ViewData["FoodId"] = new SelectList(_uow.FoodRepository.AllAsync().Result, "Id", "Description");
            ViewData["IngredientId"] = new SelectList(_uow.IngredientRepository.AllAsync().Result, "Id", "Description");
            ViewData["UnitId"] = new SelectList(_uow.UnitRepository.AllAsync().Result, "Id", "UnitName");
            return View();
        }

        // POST: FoodIngredients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodIngredient foodIngredient)
        {
            if (ModelState.IsValid)
            {
                foodIngredient.Id = Guid.NewGuid();
                _uow.FoodIngredientRepository.Add(foodIngredient);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["FoodId"] = new SelectList(_uow.FoodRepository.AllAsync().Result, "Id", "Description",
                foodIngredient.FoodId);
            ViewData["IngredientId"] = new SelectList(_uow.IngredientRepository.AllAsync().Result, "Id", "Description",
                foodIngredient.IngredientId);
            ViewData["UnitId"] = new SelectList(_uow.UnitRepository.AllAsync().Result, "Id", "UnitName",
                foodIngredient.UnitId);
            return View(foodIngredient);
        }

        // GET: FoodIngredients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodIngredient = await _uow.FoodIngredientRepository.FindAsync(id.Value);
            if (foodIngredient == null)
            {
                return NotFound();
            }

            ViewData["FoodId"] = new SelectList(_uow.FoodRepository.AllAsync().Result, "Id", "Description", foodIngredient.FoodId);
            ViewData["IngredientId"] =
                new SelectList(_uow.IngredientRepository.AllAsync().Result, "Id", "Description", foodIngredient.IngredientId);
            ViewData["UnitId"] = new SelectList(_uow.UnitRepository.AllAsync().Result, "Id", "UnitName", foodIngredient.UnitId);
            return View(foodIngredient);
        }

        // POST: FoodIngredients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FoodIngredient foodIngredient)
        {
            if (id != foodIngredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.FoodIngredientRepository.Update(foodIngredient);
                    await _uow.SaveChangesAsync();
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

            ViewData["FoodId"] = new SelectList(_uow.FoodRepository.AllAsync().Result, "Id", "Description", foodIngredient.FoodId);
            ViewData["IngredientId"] =
                new SelectList(_uow.IngredientRepository.AllAsync().Result, "Id", "Description", foodIngredient.IngredientId);
            ViewData["UnitId"] = new SelectList(_uow.UnitRepository.AllAsync().Result, "Id", "UnitName", foodIngredient.UnitId);
            return View(foodIngredient);
        }

        // GET: FoodIngredients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodIngredient = await _uow.FoodIngredientRepository.FindAsync(id.Value);
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
            var foodIngredient = await _uow.FoodIngredientRepository.FindAsync(id);
            if (foodIngredient != null)
            {
                _uow.FoodIngredientRepository.Remove(foodIngredient);
            }

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodIngredientExists(Guid id)
        {
            return (_uow.FoodIngredientRepository.AllAsync().Result?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}