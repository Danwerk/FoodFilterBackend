using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories;

public class FoodNutrientRepository: EFBaseRepository<FoodNutrient, ApplicationDbContext>, IFoodNutrientRepository
{
    public FoodNutrientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
    public override async Task<IEnumerable<FoodNutrient>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(c => c.Food)
            .Include(c => c.Nutrient)
            .Include(c => c.Unit)
            .ToListAsync();
    }

    public override async Task<FoodNutrient?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(c => c.Food)
            .Include(c => c.Nutrient)
            .Include(c => c.Unit)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    
}