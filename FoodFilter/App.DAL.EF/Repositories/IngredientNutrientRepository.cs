using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories;

public class IngredientNutrientRepository : EFBaseRepository<IngredientNutrient, ApplicationDbContext>,
    IIngredientNutrientRepository
{
    public IngredientNutrientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<IngredientNutrient> GetAll(int limit, string? search)
    {
        var query = RepositoryDbSet
            .Include(i=>i.Ingredient)
            .Include(i=>i.Unit)
            .Include(i=>i.Nutrient)
            .OrderByDescending(i => i.CreatedAt)
            .Take(limit)
            .AsNoTracking()
            .AsQueryable();

        var newQuery = query
            .AsEnumerable()
            .Where(f => ContainsSearch(f, search));

        return newQuery;
    }

    public async Task<IEnumerable<IngredientNutrient>> GetAllByIngredientIdAsync(Guid ingredientId)
    {
        return await RepositoryDbSet.Include(i => i.Ingredient)
            .Include(i => i.Unit)
            .Include(i => i.Nutrient)
            .Where(i => i.IngredientId == ingredientId)
            .ToListAsync();
    }

    private bool ContainsSearch(IngredientNutrient ingredientNutrient, string? search)
    {
        if (string.IsNullOrEmpty(search))
        {
            return true;
        }

        search = search.ToLower();
        return ingredientNutrient.Ingredient!.Name.ToLower().Contains(search) ||
               ingredientNutrient.Nutrient!.Name.ToLower().Contains(search);
    }
}