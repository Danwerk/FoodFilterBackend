using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain;
using DAL.EF;

namespace WebApp.Areas_Admin_Controllers
{
    public class OpenHoursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OpenHoursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OpenHours
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OpenHours.Include(o => o.Restaurant);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OpenHours/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.OpenHours == null)
            {
                return NotFound();
            }

            var openHours = await _context.OpenHours
                .Include(o => o.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (openHours == null)
            {
                return NotFound();
            }

            return View(openHours);
        }

        // GET: OpenHours/Create
        public IActionResult Create()
        {
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address");
            return View();
        }

        // POST: OpenHours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RestaurantId,Weekday,StartTime,EndTime,CreatedAt,UpdatedAt,Id")] OpenHours openHours)
        {
            if (ModelState.IsValid)
            {
                openHours.Id = Guid.NewGuid();
                _context.Add(openHours);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", openHours.RestaurantId);
            return View(openHours);
        }

        // GET: OpenHours/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.OpenHours == null)
            {
                return NotFound();
            }

            var openHours = await _context.OpenHours.FindAsync(id);
            if (openHours == null)
            {
                return NotFound();
            }
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", openHours.RestaurantId);
            return View(openHours);
        }

        // POST: OpenHours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RestaurantId,Weekday,StartTime,EndTime,CreatedAt,UpdatedAt,Id")] OpenHours openHours)
        {
            if (id != openHours.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(openHours);
                    await _context.SaveChangesAsync();
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
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Address", openHours.RestaurantId);
            return View(openHours);
        }

        // GET: OpenHours/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.OpenHours == null)
            {
                return NotFound();
            }

            var openHours = await _context.OpenHours
                .Include(o => o.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (_context.OpenHours == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OpenHours'  is null.");
            }
            var openHours = await _context.OpenHours.FindAsync(id);
            if (openHours != null)
            {
                _context.OpenHours.Remove(openHours);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpenHoursExists(Guid id)
        {
          return (_context.OpenHours?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
