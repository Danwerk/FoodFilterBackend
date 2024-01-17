using System.Security.Claims;
using App.BLL.Mappers;
using App.Contracts.DAL;
using AutoMapper;
using AppUser = App.BLL.DTO.Identity.AppUser;

namespace App.BLL.Services.Identity;

public class UserService
{
    private readonly IdentityBLL _identityBll;
    private readonly UserMapper _userMapper;
    

    public UserService(IdentityBLL identityBll, IMapper mapper) 
       
    {
        _identityBll = identityBll;
        _userMapper = new UserMapper(mapper);
    }

    private IAppUOW Uow => _identityBll.Uow;

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
        var users = await _identityBll.Uow.UserRepository.GetAllUsersWithRolesAsync();

        return users.Select(e => _userMapper.Map(e)!).ToList();
    }
}  