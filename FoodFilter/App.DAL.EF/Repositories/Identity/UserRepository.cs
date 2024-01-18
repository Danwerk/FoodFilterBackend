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
       return await RepositoryDbSet
            .Include(u => u.AppUserRoles!)
            .ThenInclude(u => u.AppRole)
            .ToListAsync();
    }

    public async Task<AppUser?> GetUserWithRolesAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(e => e.AppUserRoles!)
            .ThenInclude(e => e.AppRole)
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();
    }
}