using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.Domain;

public class Unit : DomainEntityId
{
    [MaxLength(128)]
    public string UnitName { get; set; } = default!;
    
    [DataType(DataType.DateTime)] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<FoodNutrient>? FoodNutrients { get; set; }
    public ICollection<FoodIngredient>? FoodIngredients { get; set; }
    public ICollection<IngredientNutrient>? IngredientNutrients { get; set; }


}