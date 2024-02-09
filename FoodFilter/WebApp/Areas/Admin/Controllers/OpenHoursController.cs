using App.Contracts.DAL;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
#pragma warning disable 1591

namespace WebApp.Areas.Admin.Controllers
{
    public class OpenHoursController : Controller
    {
        private readonly IAppUOW _uow;


        public OpenHoursController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: OpenHours
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.OpenHoursRepository.AllAsync();
            return View(vm);
        }

        // GET: OpenHours/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var openHours = await _uow.OpenHoursRepository.FindAsync(id.Value);
            if (openHours == null)
            {
                return NotFound();
            }

            return View(openHours);
        }

        // GET: OpenHours/Create
        public IActionResult Create()
        {
            ViewData["RestaurantId"] = new SelectList(_uow.RestaurantRepository.AllAsync().Result, "Id", "Address");
            return View();
        }

        // POST: OpenHours/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OpenHours openHours)
        {
            if (ModelState.IsValid)
            {
                openHours.Id = Guid.NewGuid();
                _uow.OpenHoursRepository.Add(openHours);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(_uow.RestaurantRepository.AllAsync().Result, "Id", "Address", openHours.RestaurantId);
            return View(openHours);
        }

        // GET: OpenHours/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var openHours = await _uow.OpenHoursRepository.FindAsync(id.Value);
            if (openHours == null)
            {
                return NotFound();
            }
            ViewData["RestaurantId"] = new SelectList(_uow.RestaurantRepository.AllAsync().Result, "Id", "Address", openHours.RestaurantId);
            return View(openHours);
        }

        // POST: OpenHours/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, OpenHours openHours)
        {
            if (id != openHours.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.OpenHoursRepository.Update(openHours);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpenHoursExists(openHours.Id))
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
            ViewData["RestaurantId"] = new SelectList(_uow.RestaurantRepository.AllAsync().Result, "Id", "Address", openHours.RestaurantId);
            return View(openHours);
        }

        // GET: OpenHours/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var openHours = await _uow.OpenHoursRepository.FindAsync(id.Value);
            if (openHours == null)
            {
                return NotFound();
            }

            return View(openHours);
        }

        // POST: OpenHours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var openHours = await _uow.OpenHoursRepository.FindAsync(id);
            if (openHours != null)
            {
                _uow.OpenHoursRepository.Remove(openHours);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpenHoursExists(Guid id)
        {
          return (_uow.OpenHoursRepository.AllAsync().Result?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
