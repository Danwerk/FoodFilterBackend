using App.Contracts.DAL;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    public class FoodsController : Controller
    {
        private readonly IAppUOW _uow;

        public FoodsController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Foods
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.FoodRepository.AllAsync();
            return View(vm);
        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _uow.FoodRepository.FindAsync(id.Value);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_uow.CategoryRepository.AllAsync().Result, "Id", "Name");
            ViewData["RestaurantId"] = new SelectList(_uow.RestaurantRepository.AllAsync().Result, "Id", "Address");
            return View();
        }

        // POST: Foods/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,RestaurantId,Name,Description,Price,ImageUrl,ImageData,CreatedAt,UpdatedAt,Id")] Food food)
        {
            if (ModelState.IsValid)
            {
                food.Id = Guid.NewGuid();
                _uow.FoodRepository.Add(food);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_uow.CategoryRepository.AllAsync().Result, "Id", "Name", food.CategoryId);
            ViewData["RestaurantId"] = new SelectList(_uow.RestaurantRepository.AllAsync().Result, "Id", "Address", food.RestaurantId);
            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _uow.FoodRepository.FindAsync(id.Value);
            if (food == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_uow.CategoryRepository.AllAsync().Result, "Id", "Name", food.CategoryId);
            ViewData["RestaurantId"] = new SelectList(_uow.RestaurantRepository.AllAsync().Result, "Id", "Address", food.RestaurantId);
            return View(food);
        }

        // POST: Foods/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Food food)
        {
            if (id != food.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.FoodRepository.Update(food);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.Id))
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
            ViewData["CategoryId"] = new SelectList(_uow.CategoryRepository.AllAsync().Result, "Id", "Name", food.CategoryId);
            ViewData["RestaurantId"] = new SelectList(_uow.RestaurantRepository.AllAsync().Result, "Id", "Address", food.RestaurantId);
            return View(food);
        }

        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _uow.FoodRepository.FindAsync(id.Value);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var food = await _uow.FoodRepository.FindAsync(id);
            if (food != null)
            {
                _uow.FoodRepository.Remove(food);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(Guid id)
        {
          return (_uow.FoodRepository.AllAsync().Result?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
