using App.Common;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.ViewModels;

namespace WebApp.Areas.Admin.Controllers;


[Authorize(Roles = RoleNames.Admin)]
public class UsersApprovalController : Controller
{

    private readonly UserManager<AppUser> _userManager;
    
    public UsersApprovalController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<ActionResult> Index()
    {
        var allUsersForApproval = await _userManager.Users
            .Where(u => u.IsApproved == false)
            .ToListAsync();

        var vm = allUsersForApproval
            .Where(u => _userManager.IsInRoleAsync(u, RoleNames.Restaurant).Result)
            .ToList();
        return View(new UsersApprovalViewModel
        {
            UsersForApproval = vm
        });
    }
    
    
}