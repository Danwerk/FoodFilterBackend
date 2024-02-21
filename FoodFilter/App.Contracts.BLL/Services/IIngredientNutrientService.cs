using App.BLL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IIngredientNutrientService: IBaseRepository<App.BLL.DTO.IngredientNutrient>
{
    IEnumerable<IngredientNutrient> GetAll(int limit, string? search);
}