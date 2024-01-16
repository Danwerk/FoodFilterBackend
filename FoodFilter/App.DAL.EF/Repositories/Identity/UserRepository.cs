using App.Contracts.DAL.Identity;
using App.Domain.Identity;
using Base.DAL.EF;

namespace DAL.EF.Repositories.Identity;

public class UserRepository : EFBaseRepository<AppUser, ApplicationDbContext>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}