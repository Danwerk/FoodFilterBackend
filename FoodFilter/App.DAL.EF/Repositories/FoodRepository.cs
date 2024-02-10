using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories;

public class FoodRepository : EFBaseRepository<Food, ApplicationDbContext>, IFoodRepository
{
    public FoodRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
    public override async Task<IEnumerable<Food>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(c=>c.Restaurant)
            .Include(f=>f.FoodNutrients)
            .Include(f=>f.FoodAllergens)
            .Include(f=>f.FoodIngredients)!
            .ThenInclude(fi=>fi.Ingredient)
            .ThenInclude(i => i!.IngredientNutrients)
            .ToListAsync();
    }
    
    public override async Task<Food?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(c => c.Restaurant)
            .Include(f=>f.FoodNutrients)
            .Include(f=>f.FoodIngredients)!
            .ThenInclude(fi=>fi.Ingredient)
            .ThenInclude(i => i!.IngredientNutrients)!
            .ThenInclude(i=> i.Nutrient)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}