﻿using App.BLL.DTO;
using App.Common.IngredientNutrientDtos;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IIngredientNutrientService: IBaseRepository<App.BLL.DTO.IngredientNutrient>
{
    IEnumerable<IngredientNutrient> GetAll(int limit, string? search);
    
    Task AddIngredientNutrientsForIngredient(Ingredient ingredient);
    
    Task UpdateIngredientNutrientsAsync(Guid ingredientId, List<NutrientUpdateDto>? nutrients);
    
    
}