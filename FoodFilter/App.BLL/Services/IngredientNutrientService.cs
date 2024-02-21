using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class IngredientNutrientService: BaseEntityService<App.BLL.DTO.IngredientNutrient, App.Domain.IngredientNutrient, IIngredientNutrientRepository>, IIngredientNutrientService
{
    protected IAppUOW Uow;

    public IngredientNutrientService(IAppUOW uow, IMapper<IngredientNutrient, Domain.IngredientNutrient> mapper)
        : base(uow.IngredientNutrientRepository, mapper)
    {
        Uow = uow;
    }

    public IEnumerable<IngredientNutrient> GetAll(int limit, string? search)
    {
        var ingredientNutrients = Uow.IngredientNutrientRepository.GetAll(limit, search);
        
        var ingredientNutrientDtos = ingredientNutrients.Select(r => Mapper.Map(r)).ToList();

        return ingredientNutrientDtos!;
    }
}