using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories;

public class FoodAllergenRepository: EFBaseRepository<FoodAllergen, ApplicationDbContext>, IFoodAllergenRepository
{
    public FoodAllergenRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    public override async Task<IEnumerable<FoodAllergen>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(c => c.Food)
            .Include(c => c.Allergen)
            .ToListAsync();
    }

    public override async Task<FoodAllergen?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(c => c.Food)
            .Include(c => c.Allergen)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    
}