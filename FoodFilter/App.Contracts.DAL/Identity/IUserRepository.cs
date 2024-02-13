using App.BLL.DTO.Identity;
using Base.Contracts.DAL;
using AppUser = App.Domain.Identity.AppUser;

namespace App.Contracts.DAL.Identity;

public interface IUserRepository: IBaseRepository<AppUser>
{
    public Task<IEnumerable<AppUser>> GetAllUsersWithRolesAsync();

    public Task<AppUser?> GetUserWithRolesAsync(Guid id);
    public Task<AppUser?> GetUser(string email);
    public Task<AppUser?> GetUser(Guid id);

    public Task<IEnumerable<AppUser>> GetRestaurantUsersAsync();
    
    public Task<IEnumerable<AppUser>> GetUnapprovedRestaurantUsersAsync();
}