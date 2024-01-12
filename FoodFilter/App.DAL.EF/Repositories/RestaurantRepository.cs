using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories;

public class RestaurantRepository : EFBaseRepository<Restaurant, ApplicationDbContext>, IRestaurantRepository
{
    public RestaurantRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
    public override async Task<IEnumerable<Restaurant>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(e => e.AppUser)
            .ToListAsync();
    }
    
    public virtual async Task<IEnumerable<Restaurant>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Include(e => e.AppUser)
            .Where(c => c.AppUserId == userId)
            .ToListAsync();
    }

    public override async Task<Restaurant?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(c => c.AppUser)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    
    public virtual async Task<Restaurant?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Include(c => c.AppUser)
            .FirstOrDefaultAsync(m => m.Id == id && m.AppUserId == userId);
    }
}