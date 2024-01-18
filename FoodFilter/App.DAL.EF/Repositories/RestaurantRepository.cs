using App.Common;
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
            .ThenInclude(e=> e!.AppUserRoles)!
            .ThenInclude(e=> e.AppRole)
            .Where(r => r.AppUser!.AppUserRoles!
                .Any(ur=> ur.AppRole!.Name == RoleNames.Restaurant))
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

    public async  Task<List<Restaurant>?> SearchRestaurantsAsync(string? restaurantName, string? city, string? street, string? streetNumber)
    {
        var query = RepositoryDbSet
            .Include(e => e.AppUser)
            .Where(r => r.AppUser!.AppUserRoles!
                .Any(ur => ur.AppRole!.Name == RoleNames.Restaurant));

        if (!string.IsNullOrWhiteSpace(restaurantName))
            query = query.Where(r => r.Name!.ToUpper().Contains(restaurantName.ToUpper()));

        if (!string.IsNullOrWhiteSpace(city))
            query = query.Where(r => r.City!.ToUpper().Contains(city.ToUpper()));

        if (!string.IsNullOrWhiteSpace(street))
            query = query.Where(r => r.Street!.ToUpper().Contains(street.ToUpper()));

        if (!string.IsNullOrWhiteSpace(streetNumber))
            query = query.Where(r => r.StreetNumber!.Contains(streetNumber));

        var result = await query.ToListAsync();
        return result;
    }
    
    public virtual async Task<Restaurant?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Include(c => c.AppUser)
            .FirstOrDefaultAsync(m => m.Id == id && m.AppUserId == userId);
    }
    
    public virtual async Task<Restaurant?> FindByUserIdAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Include(c => c.AppUser)
            .FirstOrDefaultAsync(m => m.AppUserId == userId);
    }
}