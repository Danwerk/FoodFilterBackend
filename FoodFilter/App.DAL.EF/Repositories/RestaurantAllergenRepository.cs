using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories;

public class RestaurantAllergenRepository : EFBaseRepository<RestaurantAllergen, ApplicationDbContext>,
    IRestaurantAllergenRepository
{
    public RestaurantAllergenRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<RestaurantAllergen>> AllAsync(Guid id)
    {
        return await RepositoryDbSet
            .Where(r => r.RestaurantId == id)
            .ToListAsync();
    }

    public async Task<RestaurantAllergen?> FindByAllergenIdAsync(Guid allergenId)
    {
        return await RepositoryDbSet
            .FirstOrDefaultAsync(i => i.AllergenId == allergenId);
    }
}