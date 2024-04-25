using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories;

public class RestaurantClaimRepository : EFBaseRepository<RestaurantClaim, ApplicationDbContext>,
    IRestaurantClaimRepository
{
    public RestaurantClaimRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<RestaurantClaim>> AllAsync(Guid id)
    {
        return await RepositoryDbSet
            .Where(r => r.RestaurantId == id)
            .ToListAsync();
    }
}
