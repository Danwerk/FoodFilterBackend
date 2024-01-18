using App.Common;
using App.Contracts.BLL;
using App.Contracts.DAL;
using App.Domain;
using App.Domain.Identity;
using Base.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IAppUOW _uow;
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;


        public RestaurantsController(UserManager<AppUser> userManager, IAppUOW uow, IAppBLL bll)
        {
            _userManager = userManager;
            _uow = uow;
            _bll = bll;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index([FromQuery] RestaurantSearchModel searchModel)
        {
            var searchResult = await _bll.RestaurantService.SearchRestaurantsAsync(searchModel.RestaurantName,
                searchModel.City, searchModel.Street, searchModel.StreetNumber);
            searchModel.SearchResult = searchResult;

            return View(searchModel);
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

            ViewData["AppUserId"] =
                new SelectList(_userManager.Users, nameof(AppUser.Id), "FirstName", restaurant.AppUserId);
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

            var currentUserId = User.GetUserId();

            // Check if the logged-in user is the owner of the restaurant
            if (!User.IsInRole(RoleNames.Admin))
            {
                if (restaurant.AppUserId != currentUserId)
                {
                    // If not the owner, deny access
                    return Forbid();
                }
            }

            ViewData["AppUserId"] = new SelectList(_userManager.Users, nameof(restaurant.AppUserId), "FirstName",
                restaurant.AppUserId);
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
                    restaurant.AppUserId = User.GetUserId();
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

            ViewData["AppUserId"] =
                new SelectList(_userManager.Users, nameof(AppUser.Id), "FirstName", restaurant.AppUserId);
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