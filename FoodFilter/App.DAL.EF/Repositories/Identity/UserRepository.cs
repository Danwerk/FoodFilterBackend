using App.BLL.DTO.Identity;
using App.Contracts.DAL.Identity;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using AppUser = App.Domain.Identity.AppUser;

namespace DAL.EF.Repositories.Identity;

public class UserRepository : EFBaseRepository<AppUser, ApplicationDbContext>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }


    public async Task<IEnumerable<AppUser>> GetAllUsersWithRolesAsync()
    {
        var m = await RepositoryDbSet
            .Include(u => u.AppUserRoles!)
            .ThenInclude(u => u.AppRole)
            .ToListAsync();
        return m;
    }
}