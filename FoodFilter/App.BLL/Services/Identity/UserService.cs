using System.Security.Claims;
using App.BLL.Mappers;
using App.Common;
using App.Contracts.DAL;
using App.Domain;
using AutoMapper;
using Base.Helpers;
using AppUser = App.BLL.DTO.Identity.AppUser;
using Restaurant = App.BLL.DTO.Restaurant;

namespace App.BLL.Services.Identity;

public class UserService
{
    private readonly IdentityBLL _identityBll;
    private readonly UserMapper _userMapper;
    private readonly RestaurantMapper _restaurantMapper;
    

    public UserService(IdentityBLL identityBll, IMapper mapper) 
       
    {
        _identityBll = identityBll;
        _userMapper = new UserMapper(mapper);
        _restaurantMapper = new RestaurantMapper(mapper);
    }

    private IAppUOW Uow => _identityBll.Uow;

    // Approve user and create restaurant for recently approved user.
    public async Task ApproveUserAsync(Guid userId)
    {
        var user = await _identityBll.UserManager.FindByIdAsync(userId.ToString());
        if (user != null)
        {
            user.IsApproved = true;
            await _identityBll.UserManager.UpdateAsync(user);
        }
        
        _userMapper.Map(user);
    }

    public async Task<AppUser?> GetUser(string email)
    {
        var user = await Uow.UserRepository.GetUser(email);
        
        return _userMapper.Map(user);
    }
    
    
    public async Task<AppUser?> GetUser(Guid id)
    {
        var user = await Uow.UserRepository.GetUser(id);
        
        return _userMapper.Map(user);
    }
    
    public async Task<AppUser?> GetCurrentAuthorizedUser(ClaimsPrincipal userPrincipal)
    {
        var user = await _identityBll.UserManager.GetUserAsync(userPrincipal);
        
        return _userMapper.Map(user);
    }

    public async Task<string> GetCurrentUserRole(ClaimsPrincipal userPrincipal)
    {
        var user = await _identityBll.UserManager.GetUserAsync(userPrincipal);
        if (user != null)
        {
            var roles = await _identityBll.UserManager.GetRolesAsync(user);
            return string.Join(", ", roles);
        }

        return string.Empty;
    }
    


  

    public async Task<IEnumerable<AppUser>> GetUsersWithRolesAsync()
    {
        var users = await Uow.UserRepository.GetAllUsersWithRolesAsync();

        return users.Select(e => _userMapper.Map(e)!).ToList();
    }
    
    public async Task<AppUser?> GetUserWithRoles(Guid id)
    {
        return _userMapper.Map(await Uow.UserRepository.GetUserWithRolesAsync(id));
    }

    public async Task CreateRestaurantForApprovedRestaurantUserAsync(Guid userId, Guid approverId)
    {
        var approvedUser = await Uow.UserRepository.FindAsync(userId);

        if (approvedUser != null && await _identityBll.UserManager.IsInRoleAsync(approvedUser, RoleNames.Restaurant))
        {
            CreateRestaurant(userId, approverId);
            await Uow.SaveChangesAsync();
        }
    }

    public Restaurant CreateRestaurant(Guid userId, Guid approverId)
    {
        var id = Guid.NewGuid();
        var restaurant = new Domain.Restaurant
        {
            Id = id,
            AppUserId = userId,
            ApprovedById = approverId,
            Name = "",
            City = "",
            Street = "",
            StreetNumber = ""
        };
        return _restaurantMapper.Map(Uow.RestaurantRepository.Add(restaurant))!;
    }
}  