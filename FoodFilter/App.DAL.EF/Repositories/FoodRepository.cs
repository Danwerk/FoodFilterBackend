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

    public async Task<IEnumerable<Food>> AllAsync(Guid restaurantId)
    {
        return await RepositoryDbSet
            .Include(c=> c.Restaurant)
            .Where(c=>c.RestaurantId == restaurantId)
            .Select(f => new Food
            {
                Id = f.Id,
                RestaurantId = f.RestaurantId,
                Name = f.Name,
                Price = f.Price,
                Images = f.Images,
                
            })
            .ToListAsync();
    }

    // public async Task<Food> Edit(Food entity)
    // {
    //     var existingEntity = RepositoryDbSet.Find(entity.Id);
    //
    //     if (existingEntity != null)
    //     {
    //         RepositoryDbContext.Entry(existingEntity).State = EntityState.Detached;
    //     }
    //
    //     return RepositoryDbSet.Update(entity).Entity;
    // }

    public override async Task<Food?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(c => c.Restaurant)
            .Include(f=>f.FoodAllergens)!
            .ThenInclude(fa=> fa.Allergen)
            .Include(f => f.FoodNutrients)!
            .ThenInclude(f=>f.Nutrient)
            .Include(f => f.FoodIngredients)!
            .ThenInclude(fi => fi.Ingredient)
            .ThenInclude(i => i!.IngredientNutrients)!
            .ThenInclude(inut => inut.Unit)  // Include Unit for IngredientNutrient
            .ThenInclude(i => i!.IngredientNutrients)!  // Redundant
            .ThenInclude(inut => inut.Nutrient)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}