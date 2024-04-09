using App.BLL.DTO;
using App.Common.IngredientNutrientDtos;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class IngredientNutrientService :
    BaseEntityService<App.BLL.DTO.IngredientNutrient, App.Domain.IngredientNutrient, IIngredientNutrientRepository>,
    IIngredientNutrientService
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

    public Task AddIngredientNutrientsForIngredient(Ingredient ingredient)
    {
        var nutrients = Uow.NutrientRepository.AllAsync().Result;

        foreach (var nutrient in nutrients)
        {
            var ingredientNutrient = new IngredientNutrient
            {
                IngredientId = ingredient.Id,
                NutrientId = nutrient.Id,
                Amount = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            Uow.IngredientNutrientRepository.Add(Mapper.Map(ingredientNutrient)!);
        }

        return Uow.SaveChangesAsync();
    }

    public async Task UpdateIngredientNutrientsAsync(Guid ingredientId, List<NutrientUpdateDto>? nutrients)
    {
        if (nutrients == null || !nutrients.Any())
        {
            return;
        }

        var existingIngredientNutrients =
            await Uow.IngredientNutrientRepository.GetAllByIngredientIdAsync(ingredientId);

        foreach (var nutrient in nutrients)
        {
            var existingNutrient = existingIngredientNutrients.FirstOrDefault(n => n.Nutrient!.Name == nutrient.Name);
            
            if (existingNutrient != null)
            {
                existingNutrient.Amount = nutrient.Amount;
                existingNutrient.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                // If the nutrient does not exist, you may choose to add it or skip it
               
                continue;
            }
        }

        await Uow.SaveChangesAsync();
    }
}