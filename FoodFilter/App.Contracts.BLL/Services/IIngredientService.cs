using App.BLL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IIngredientService: IBaseRepository<App.BLL.DTO.Ingredient>
{
    Task<List<string>> GetIngredientNamesAsync(List<Guid> ids);

    IEnumerable<Ingredient> GetAll(int limit, string? search);
    Task<List<Ingredient>> GetUnconfirmedIngredients();
    Task<List<Ingredient>> GetConfirmedIngredients();

}