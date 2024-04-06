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

    public IEnumerable<Food> GetAll(Guid id, int limit, string? search)
    {
        var query = RepositoryDbSet
            .Include(c=>c.Restaurant)
            .Include(c=>c.Images)
            .Where(f=>f.RestaurantId == id)
            .OrderByDescending(i => i.CreatedAt)
            .Take(limit)
            .AsNoTracking()
            .AsQueryable();

        var newQuery = query
            .AsEnumerable()
            .Where(f => ContainsSearch(f, search));

        return newQuery.Select(f=> new Food
        {
            Id = f.Id,
            RestaurantId = f.RestaurantId,
            Name = f.Name,
            Price = f.Price,
            Images = f.Images,
            IsPublished = f.IsPublished
        });
    }
    
    private bool ContainsSearch(Food food, string? search)
    {
        if (string.IsNullOrEmpty(search))
        {
            return true;
        }
        search = search.ToLower();
        return food.Name.ToLower().Contains(search);
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
                IsPublished = f.IsPublished
                
            })
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Food>> PublishedAllAsync(Guid restaurantId)
    {
        return await RepositoryDbSet
            .Include(c=> c.Restaurant)
            .Where(c=>c.RestaurantId == restaurantId && c.IsPublished == true)
            .Select(f => new Food
            {
                Id = f.Id,
                RestaurantId = f.RestaurantId,
                Name = f.Name,
                Price = f.Price,
                Images = f.Images,
                IsPublished = f.IsPublished
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
            .Include(f=>f.Images)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}