using App.Contracts.DAL;
using App.Domain;
using DAL.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
#pragma warning disable 1591

namespace WebApp.Areas.Admin.Controllers
{
    public class FoodAllergensController : Controller
    {
        private readonly IAppUOW _uow;

        public FoodAllergensController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: FoodAllergens
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.FoodAllergenRepository.AllAsync();
            return View(vm);
        }

        // GET: FoodAllergens/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodAllergen = await _uow.FoodAllergenRepository.FindAsync(id.Value);
            if (foodAllergen == null)
            {
                return NotFound();
            }

            return View(foodAllergen);
        }

        // GET: FoodAllergens/Create
        public IActionResult Create()
        {
            ViewData["AllergenId"] = new SelectList(_uow.AllergenRepository.AllAsync().Result, "Id", "Id");
            ViewData["FoodId"] = new SelectList(_uow.FoodRepository.AllAsync().Result, "Id", "Description");
            return View();
        }

        // POST: FoodAllergens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodAllergen foodAllergen)
        {
            if (ModelState.IsValid)
            {
                foodAllergen.Id = Guid.NewGuid();
                _uow.FoodAllergenRepository.Add(foodAllergen);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AllergenId"] = new SelectList(_uow.AllergenRepository.AllAsync().Result, "Id", "Id", foodAllergen.AllergenId);
            ViewData["FoodId"] = new SelectList(_uow.FoodRepository.AllAsync().Result, "Id", "Description", foodAllergen.FoodId);
            return View(foodAllergen);
        }

        // GET: FoodAllergens/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodAllergen = await _uow.FoodAllergenRepository.FindAsync(id.Value);
            if (foodAllergen == null)
            {
                return NotFound();
            }
            ViewData["AllergenId"] = new SelectList(_uow.AllergenRepository.AllAsync().Result, "Id", "Id", foodAllergen.AllergenId);
            ViewData["FoodId"] = new SelectList(_uow.FoodRepository.AllAsync().Result, "Id", "Description", foodAllergen.FoodId);
            return View(foodAllergen);
        }

        // POST: FoodAllergens/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FoodAllergen foodAllergen)
        {
            if (id != foodAllergen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.FoodAllergenRepository.Update(foodAllergen);
                    await _uow.SaveChangesAsync();
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
            ViewData["AllergenId"] = new SelectList(_uow.AllergenRepository.AllAsync().Result, "Id", "Id", foodAllergen.AllergenId);
            ViewData["FoodId"] = new SelectList(_uow.FoodRepository.AllAsync().Result, "Id", "Description", foodAllergen.FoodId);
            return View(foodAllergen);
        }

        // GET: FoodAllergens/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodAllergen = await _uow.FoodAllergenRepository.FindAsync(id.Value);
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
           
            var foodAllergen = await _uow.FoodAllergenRepository.FindAsync(id);
            if (foodAllergen != null)
            {
                _uow.FoodAllergenRepository.Remove(foodAllergen);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodAllergenExists(Guid id)
        {
          return (_uow.FoodAllergenRepository.AllAsync().Result?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
