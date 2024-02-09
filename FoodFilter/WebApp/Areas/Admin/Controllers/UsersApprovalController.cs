using App.BLL.Services.Identity;
using App.Common;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.ViewModels;
#pragma warning disable 1591

namespace WebApp.Areas.Admin.Controllers;


[Authorize(Roles = RoleNames.Admin)]
public class UsersApprovalController : Controller
{

    private readonly IdentityBLL _bll;
    
    public UsersApprovalController(IdentityBLL bll)
    {
        _bll = bll;
    }
    
    public async Task<ActionResult> Index()
    {
        var allUsersForApproval = await _bll.UserManager.Users
            .Where(u => u.IsApproved == false)
            .ToListAsync();

        var vm = allUsersForApproval
            .Where(u => _bll.UserManager.IsInRoleAsync(u, RoleNames.Restaurant).Result)
            .ToList();
        return View(new UsersApprovalViewModel
        {
            UsersForApproval = vm,
            CurrentUserRole = _bll.UserService.GetCurrentUserRole(User)
        });
    }
    
    
    [HttpPost]
    [HttpGet]
    public async Task<IActionResult> Approve(Guid id)
    {
        await _bll.UserService.ApproveUserAsync(id);
        
        await _bll.UserService.CreateRestaurantForApprovedRestaurantUserAsync(id, User.GetUserId());
        
        await _bll.SaveChangesAsync();
        // Redirect back to the index page or any other page as needed
        return RedirectToAction(nameof(Index));
    }
    
    // // GET: UsersApproval/Delete/5
    // public async Task<IActionResult> Delete(Guid? id)
    // {
    //     if (id == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     var unit = await _uow.UnitRepository.FindAsync(id.Value);
    //     if (unit == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     return View(unit);
    // }
    //
    // // POST: Units/Delete/5
    // [HttpPost, ActionName("Delete")]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> DeleteConfirmed(Guid id)
    // {
    //     var unit = await _uow.UnitRepository.FindAsync(id);
    //     if (unit != null)
    //     {
    //         _uow.UnitRepository.Remove(unit);
    //     }
    //         
    //     await _uow.SaveChangesAsync();
    //     return RedirectToAction(nameof(Index));
    // }
    
    
}