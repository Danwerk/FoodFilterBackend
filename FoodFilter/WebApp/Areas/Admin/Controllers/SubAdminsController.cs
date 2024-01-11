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
    public class SubAdminsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubAdminsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SubAdmins
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SubAdmins.Include(s => s.AppUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SubAdmins/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.SubAdmins == null)
            {
                return NotFound();
            }

            var subAdmin = await _context.SubAdmins
                .Include(s => s.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subAdmin == null)
            {
                return NotFound();
            }

            return View(subAdmin);
        }

        // GET: SubAdmins/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: SubAdmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,Region,Languages,Productivity,Id")] SubAdmin subAdmin)
        {
            if (ModelState.IsValid)
            {
                subAdmin.Id = Guid.NewGuid();
                _context.Add(subAdmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", subAdmin.AppUserId);
            return View(subAdmin);
        }

        // GET: SubAdmins/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.SubAdmins == null)
            {
                return NotFound();
            }

            var subAdmin = await _context.SubAdmins.FindAsync(id);
            if (subAdmin == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", subAdmin.AppUserId);
            return View(subAdmin);
        }

        // POST: SubAdmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,Region,Languages,Productivity,Id")] SubAdmin subAdmin)
        {
            if (id != subAdmin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subAdmin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubAdminExists(subAdmin.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", subAdmin.AppUserId);
            return View(subAdmin);
        }

        // GET: SubAdmins/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.SubAdmins == null)
            {
                return NotFound();
            }

            var subAdmin = await _context.SubAdmins
                .Include(s => s.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subAdmin == null)
            {
                return NotFound();
            }

            return View(subAdmin);
        }

        // POST: SubAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.SubAdmins == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SubAdmins'  is null.");
            }
            var subAdmin = await _context.SubAdmins.FindAsync(id);
            if (subAdmin != null)
            {
                _context.SubAdmins.Remove(subAdmin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubAdminExists(Guid id)
        {
          return (_context.SubAdmins?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
