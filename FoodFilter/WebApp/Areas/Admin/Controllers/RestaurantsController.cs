using App.Contracts.DAL;
using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IAppUOW _uow;
        private readonly UserManager<AppUser> _userManager;


        public RestaurantsController(UserManager<AppUser> userManager, IAppUOW uow)
        {
            _userManager = userManager;
            _uow = uow;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.RestaurantRepository.AllAsync();
            return View(vm);
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _uow.RestaurantRepository.FindAsync(id.Value);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_userManager.Users, nameof(AppUser.Id), "Email");
            return View();
        }

        // POST: Restaurants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                restaurant.Id = Guid.NewGuid();
                _uow.RestaurantRepository.Add(restaurant);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_userManager.Users, nameof(AppUser.Id), "FirstName", restaurant.AppUserId);
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _uow.RestaurantRepository.FindAsync(id.Value);
            if (restaurant == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_userManager.Users, nameof(AppUser.Id), "FirstName", restaurant.AppUserId);
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.RestaurantRepository.Update(restaurant);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.Id))
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
            ViewData["AppUserId"] = new SelectList(_userManager.Users, nameof(AppUser.Id), "FirstName", restaurant.AppUserId);
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _uow.RestaurantRepository.FindAsync(id.Value);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var restaurant = await _uow.RestaurantRepository.FindAsync(id);
            if (restaurant != null)
            {
                _uow.RestaurantRepository.Remove(restaurant);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(Guid id)
        {
          return (_uow.RestaurantRepository.AllAsync().Result?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
