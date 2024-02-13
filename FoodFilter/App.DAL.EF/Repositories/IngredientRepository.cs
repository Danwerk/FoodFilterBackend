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
                UnitName = i.Unit!.UnitName
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
}