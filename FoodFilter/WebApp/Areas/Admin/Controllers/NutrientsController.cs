using App.Contracts.DAL;
using App.Domain;
using DAL.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    public class NutrientsController : Controller
    {
        private readonly IAppUOW _uow;

        public NutrientsController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Nutrients
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.NutrientRepository.AllAsync();
            return View(vm);
        }

        // GET: Nutrients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrient = await _uow.NutrientRepository.FindAsync(id.Value);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Nutrient nutrient)
        {
            if (ModelState.IsValid)
            {
                nutrient.Id = Guid.NewGuid();
                nutrient.CreatedAt = DateTime.SpecifyKind(nutrient.CreatedAt, DateTimeKind.Utc);
                nutrient.UpdatedAt = DateTime.SpecifyKind(nutrient.UpdatedAt, DateTimeKind.Utc);
                _uow.NutrientRepository.Add(nutrient);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nutrient);
        }

        // GET: Nutrients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nutrient = await _uow.NutrientRepository.FindAsync(id.Value);
            if (nutrient == null)
            {
                return NotFound();
            }
            return View(nutrient);
        }

        // POST: Nutrients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Nutrient nutrient)
        {
            if (id != nutrient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.NutrientRepository.Update(nutrient);
                    await _uow.SaveChangesAsync();
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
            if (id == null)
            {
                return NotFound();
            }

            var nutrient = await _uow.NutrientRepository.FindAsync(id.Value);
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
            var nutrient = await _uow.NutrientRepository.FindAsync(id);
            if (nutrient != null)
            {
                _uow.NutrientRepository.Remove(nutrient);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NutrientExists(Guid id)
        {
          return (_uow.NutrientRepository.AllAsync().Result?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
