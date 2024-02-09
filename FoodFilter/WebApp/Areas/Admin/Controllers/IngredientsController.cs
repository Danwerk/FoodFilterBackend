using App.Contracts.DAL;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
#pragma warning disable 1591

namespace WebApp.Areas.Admin.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly IAppUOW _uow;

        public IngredientsController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Ingredients
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.IngredientRepository.AllAsync();
            return View(vm);
        }

        // GET: Ingredients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _uow.IngredientRepository.FindAsync(id.Value);
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // GET: Ingredients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ingredients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                ingredient.Id = Guid.NewGuid();
                ingredient.CreatedAt = DateTime.SpecifyKind(ingredient.CreatedAt, DateTimeKind.Utc);
                ingredient.UpdatedAt = DateTime.SpecifyKind(ingredient.UpdatedAt, DateTimeKind.Utc);
                _uow.IngredientRepository.Add(ingredient);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        // GET: Ingredients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _uow.IngredientRepository.FindAsync(id.Value);
            if (ingredient == null)
            {
                return NotFound();
            }
            return View(ingredient);
        }

        // POST: Ingredients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Ingredient ingredient)
        {
            if (id != ingredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.IngredientRepository.Update(ingredient);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientExists(ingredient.Id))
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
            return View(ingredient);
        }

        // GET: Ingredients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _uow.IngredientRepository.FindAsync(id.Value);
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ingredient = await _uow.IngredientRepository.FindAsync(id);
            if (ingredient != null)
            {
                _uow.IngredientRepository.Remove(ingredient);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientExists(Guid id)
        {
          return (_uow.IngredientRepository.AllAsync().Result?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
