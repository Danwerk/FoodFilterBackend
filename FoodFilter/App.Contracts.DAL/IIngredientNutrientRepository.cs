using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IIngredientNutrientRepository: IBaseRepository<IngredientNutrient>
{
    IEnumerable<IngredientNutrient> GetAll(int limit, string? search);
    
    Task<IEnumerable<IngredientNutrient>> GetAllByIngredientIdAsync(Guid ingredientId);

    Task AddRangeAsync(IEnumerable<IngredientNutrient> ingredientNutrients);
}