using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class IngredientService :
    BaseEntityService<App.BLL.DTO.Ingredient, App.Domain.Ingredient, IIngredientRepository>, IIngredientService
{
    protected IAppUOW Uow;

    public IngredientService(IAppUOW uow, IMapper<Ingredient, Domain.Ingredient> mapper)
        : base(uow.IngredientRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<List<string>> GetIngredientNamesAsync(List<Guid> ids)
    {
        var ingredients = await Uow.IngredientRepository.GetIngredientsByIdsAsync(ids);

        return ingredients.Select(i => i.Name).ToList();
    }

    public async Task<IEnumerable<Ingredient>> AllAsync(Guid restaurantId)
    {
        var ingredients = await Uow.IngredientRepository.AllAsync(restaurantId);
        
        var ingredientDtos = ingredients.Select(r => Mapper.Map(r)).ToList();

        return ingredientDtos!;
    }

    public IEnumerable<Ingredient> GetAll(int limit, string? search)
    {
        var ingredients = Uow.IngredientRepository.GetAll(limit, search);
        
        var ingredientDtos = ingredients.Select(r => Mapper.Map(r)).ToList();

        return ingredientDtos!;
        
    }

    public async Task<List<Ingredient>> GetUnconfirmedIngredients()
    {
        var ingredients = await Uow.IngredientRepository.GetUnconfirmedIngredients();
        
        var ingredientDtos = ingredients.Select(r => Mapper.Map(r)).ToList();

        return ingredientDtos!;
    }
    
    public async Task<List<Ingredient>> GetConfirmedIngredients()
    {
        var ingredients = await Uow.IngredientRepository.GetConfirmedIngredients();
        
        var ingredientDtos = ingredients.Select(r => Mapper.Map(r)).ToList();

        return ingredientDtos!;
    }
}