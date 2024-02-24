using App.Common.NutrientCalculationDtos;
using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories;

public class IngredientRepository : EFBaseRepository<Ingredient, ApplicationDbContext>, IIngredientRepository
{
    public IngredientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<List<Ingredient>> GetIngredientsByIdsAsync(List<Guid> ingredientIds)
    {
        return await RepositoryDbSet
            .Where(i => ingredientIds.Contains(i.Id))
            .ToListAsync();
    }

    public List<IngredientNutrientDto> GetNutrientsForIngredients(List<Guid> ingredientIds)
    {
        return RepositoryDbContext.IngredientNutrient
            .Where(i => ingredientIds.Contains(i.IngredientId))
            .Select(i => new IngredientNutrientDto()
            {
                NutrientName = i.Nutrient!.Name,
                NutrientId = i.NutrientId,
                IngredientId = i.IngredientId,
                Amount = i.Amount,
                UnitName = i.Unit!.UnitName,
                UnitId = i.UnitId
            })
            .ToList();
    }

    public List<string> GetIngredientNames(List<Guid> ingredientIds)
    {
        return RepositoryDbSet
            .Where(i => ingredientIds.Contains(i.Id))
            .Select(i => i.Name)
            .ToList();
    }

    public IEnumerable<Ingredient> GetAll(int limit, string? search)
    {
        var query = RepositoryDbSet
            .OrderByDescending(i => i.CreatedAt)
            .Take(limit)
            .AsNoTracking()
            .AsQueryable();

        var newQuery = query
            .AsEnumerable()
            .Where(f => ContainsSearch(f, search));

        return newQuery;
    }

    public async Task<List<Ingredient>> GetUnconfirmedIngredients()
    {
        return await RepositoryDbSet
            .OrderBy(i=>i.CreatedAt)
            .Where(i => !i.IsConfirmed)
            .ToListAsync();
    }
    
    public async Task<List<Ingredient>> GetConfirmedIngredients()
    {
        return await RepositoryDbSet
            .OrderByDescending(i=>i.CreatedAt)
            .Where(i => i.IsConfirmed)
            .ToListAsync();
    }

    private bool ContainsSearch(Ingredient ingredient, string? search)
    {
        if (string.IsNullOrEmpty(search))
        {
            return true;
        }
        search = search.ToLower();
        return ingredient.Name.ToLower().Contains(search) || 
               ingredient.Description == null || 
               ingredient.Description.ToLower().Contains(search);
    }

}