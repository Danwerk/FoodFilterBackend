﻿using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.BLL.DTO;

public class Ingredient: DomainEntityId
{
    public string Name { get; set; } = default!;
    
    public bool IsConfirmed { get; set; }
    
    public decimal KCaloriesPer100Grams { get; set; }
    
    public  Guid AddedBy { get; set; }

    public ICollection<FoodIngredient>? FoodIngredients { get; set; }
    public ICollection<IngredientNutrient>? IngredientNutrients { get; set; }
}