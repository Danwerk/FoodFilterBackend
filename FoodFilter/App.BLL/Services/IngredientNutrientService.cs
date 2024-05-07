using App.Common;
using App.Common.IngredientNutrientDtos;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Domain;
using Base.BLL;
using Base.Contracts;
using Ingredient = App.BLL.DTO.Ingredient;
using IngredientNutrient = App.BLL.DTO.IngredientNutrient;

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

    public async Task AddIngredientNutrientsForIngredient(Ingredient ingredient)
    {
        var nutrients = Uow.NutrientRepository.AllAsync().Result;
        var unit = await Uow.UnitRepository.FirstOrDefaultAsync(UnitTypes.G);
        Guid unitId;
        if (unit == null)
        {
            var unitToSave = new Unit()
            {
                UnitName = UnitTypes.G
            };
            Uow.UnitRepository.Add(unitToSave);
            await Uow.SaveChangesAsync();
            unitId = unitToSave.Id;
        }
        else
        {
            unitId = unit!.Id;
        }

        foreach (var nutrient in nutrients)
        {
            var ingredientNutrient = new IngredientNutrient
            {
                IngredientId = ingredient.Id,
                NutrientId = nutrient.Id,
                UnitId = unitId,
                Amount = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            Uow.IngredientNutrientRepository.Add(Mapper.Map(ingredientNutrient)!);
        }

        await Uow.SaveChangesAsync();
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