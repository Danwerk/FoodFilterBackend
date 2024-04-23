using App.Common.NutrientCalculationDtos;
using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IIngredientRepository : IBaseRepository<Ingredient>
{
    Task<List<Ingredient>> GetIngredientsByIdsAsync(List<Guid> ids);
    List<IngredientNutrientDto> GetNutrientsForIngredients(List<Guid> ingredientIds);
    List<string> GetIngredientNames(List<Guid> ingredientIds);
    IEnumerable<Ingredient> GetAll(int limit, string? search);
    Task<List<Ingredient>> GetUnconfirmedIngredients();
    Task<List<Ingredient>> GetConfirmedIngredients();
    Task AddRangeAsync(IEnumerable<Ingredient> ingredients);    
    Task<Ingredient?> FirstOrDefaultAsync(string name);



}