using App.BLL.DTO.Identity;
using App.Contracts.BLL;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using AppUser = App.Domain.Identity.AppUser;

namespace App.BLL.Services.Identity;

public class IdentityBLL : BaseBLL<IAppUOW>
{
    private readonly IServiceProvider _services;

    public IdentityBLL(IAppUOW uow, IServiceProvider services, IMapper mapper) : base(uow)
    {
        _services = services;
    }
    
    private UserManager<Domain.Identity.AppUser>? _userManager;
    public UserManager<Domain.Identity.AppUser> UserManager => _userManager ??= _services.GetRequiredService<UserManager<AppUser>>();

    private UserService? _userService;
    
    public UserService UserService => _userService ??= _services.GetRequiredService<UserService>();
}