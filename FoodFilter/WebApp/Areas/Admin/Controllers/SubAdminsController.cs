using App.Contracts.DAL;
using App.Domain;
using App.Domain.Identity;
using DAL.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    public class SubAdminsController : Controller
    {
        private readonly IAppUOW _uow;
        private readonly UserManager<AppUser> _userManager;


        public SubAdminsController(UserManager<AppUser> userManager, IAppUOW uow)
        {
            _userManager = userManager;
            _uow = uow;
        }

        // GET: SubAdmins
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.SubAdminRepository.AllAsync();
            return View(vm);
        }

        // GET: SubAdmins/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subAdmin = await _uow.SubAdminRepository.FindAsync(id.Value);
            if (subAdmin == null)
            {
                return NotFound();
            }

            return View(subAdmin);
        }

        // GET: SubAdmins/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_userManager.Users, nameof(AppUser.Id), "FirstName");
            return View();
        }

        // POST: SubAdmins/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubAdmin subAdmin)
        {
            if (ModelState.IsValid)
            {
                subAdmin.Id = Guid.NewGuid();
                _uow.SubAdminRepository.Add(subAdmin);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AppUserId"] = new SelectList(_userManager.Users, nameof(AppUser.Id), "FirstName", subAdmin.AppUserId);
            return View(subAdmin);
        }

        // GET: SubAdmins/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subAdmin = await _uow.SubAdminRepository.FindAsync(id.Value);
            if (subAdmin == null)
            {
                return NotFound();
            }

            ViewData["AppUserId"] = new SelectList(_userManager.Users, nameof(AppUser.Id), "FirstName", subAdmin.AppUserId);
            return View(subAdmin);
        }

        // POST: SubAdmins/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SubAdmin subAdmin)
        {
            if (id != subAdmin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.SubAdminRepository.Update(subAdmin);
                    await _uow.SaveChangesAsync();
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

            ViewData["AppUserId"] = new SelectList(_userManager.Users, nameof(AppUser.Id), "FirstName", subAdmin.AppUserId);
            return View(subAdmin);
        }

        // GET: SubAdmins/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subAdmin = await _uow.SubAdminRepository.FindAsync(id.Value);
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
            var subAdmin = await _uow.SubAdminRepository.FindAsync(id);
            if (subAdmin != null)
            {
                _uow.SubAdminRepository.Remove(subAdmin);
            }

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubAdminExists(Guid id)
        {
            return (_uow.SubAdminRepository.AllAsync().Result?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}