using App.BLL.Services.Identity;
using App.Common;
using App.Domain;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.ViewModels;
#pragma warning disable 1591

namespace WebApp.Areas.Admin.Controllers;

[Authorize(Roles = RoleNames.Admin)]
public class UsersController : Controller
{
    private readonly IdentityBLL _bll;

    public UsersController(IdentityBLL bll)
    {
        _bll = bll;
    }

    public async Task<IActionResult> Index()
    {
        var allUsers = (await _bll.UserService.GetUsersWithRolesAsync())
            .Where(u => u.Id != User.GetUserId())
            .ToList();

        return View(new UsersViewModel()
        {
            Users = allUsers
        });
    }

    
}