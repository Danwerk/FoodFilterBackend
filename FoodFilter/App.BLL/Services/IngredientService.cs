using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class IngredientService: BaseEntityService<App.BLL.DTO.Ingredient, App.Domain.Ingredient, IIngredientRepository >, IIngredientService
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
}  