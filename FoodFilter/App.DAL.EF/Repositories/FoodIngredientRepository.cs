using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories;

public class FoodIngredientRepository: EFBaseRepository<FoodIngredient, ApplicationDbContext>, IFoodIngredientRepository
{
    public FoodIngredientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
    public override async Task<IEnumerable<FoodIngredient>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(c => c.Food)
            .Include(c => c.Ingredient)
            .Include(c => c.Unit)
            .ToListAsync();
    }

    public override async Task<FoodIngredient?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(c => c.Food)
            .Include(c => c.Ingredient)
            .Include(c => c.Unit)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    
}