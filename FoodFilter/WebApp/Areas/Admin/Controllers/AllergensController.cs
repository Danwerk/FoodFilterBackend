using App.Contracts.DAL;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
#pragma warning disable 1591

namespace WebApp.Areas.Admin.Controllers
{
    public class AllergensController : Controller
    {
        private readonly IAppUOW _uow;

        public AllergensController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Allergens
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.AllergenRepository.AllAsync();
            return View(vm);
        }

        // GET: Allergens/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allergen = await _uow.AllergenRepository.FindAsync(id.Value);
            if (allergen == null)
            {
                return NotFound();
            }

            return View(allergen);
        }

        // GET: Allergens/Create
        public IActionResult Create()
        {
            ViewData["IngredientId"] = new SelectList(_uow.IngredientRepository.AllAsync().Result, "Id", "Description");
            return View();
        }

        // POST: Allergens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Allergen allergen)
        {
            if (ModelState.IsValid)
            {
                allergen.Id = Guid.NewGuid();
                allergen.CreatedAt = DateTime.SpecifyKind(allergen.CreatedAt, DateTimeKind.Utc);
                allergen.UpdatedAt = DateTime.SpecifyKind(allergen.UpdatedAt, DateTimeKind.Utc);
                _uow.AllergenRepository.Add(allergen);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            
            return View(allergen);
        }

        // GET: Allergens/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allergen = await _uow.AllergenRepository.FindAsync(id.Value);
            if (allergen == null)
            {
                return NotFound();
            }

            
            return View(allergen);
        }

        // POST: Allergens/Edit/5
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
                    _uow.AllergenRepository.Update(allergen);
                    await _uow.SaveChangesAsync();
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

           
            return View(allergen);
        }

        // GET: Allergens/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allergen = await _uow.AllergenRepository.FindAsync(id.Value);
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
            var allergen = await _uow.AllergenRepository.FindAsync(id);
            if (allergen != null)
            {
                _uow.AllergenRepository.Remove(allergen);
            }

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllergenExists(Guid id)
        {
            return (_uow.AllergenRepository.AllAsync().Result?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}