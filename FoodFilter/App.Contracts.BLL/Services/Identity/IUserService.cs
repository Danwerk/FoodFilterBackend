using App.Contracts.DAL;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services.Identity;

public interface IUserService: IBaseRepository<App.BLL.DTO.Identity.AppUser>
{
    // add your custom service methods here
    
    Task<App.BLL.DTO.Identity.AppUser?> ApproveUserAsync(Guid productId);
}