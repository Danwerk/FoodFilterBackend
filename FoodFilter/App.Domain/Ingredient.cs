using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.Domain;

public class Ingredient : DomainEntityId
{
    [MaxLength(256)]
    public string Name { get; set; } = default!;
    
    public bool IsConfirmed { get; set; }
    
    public decimal? KCaloriesPer100Grams { get; set; }

    // Declaring default values for createdAt and updatedAt, if values are not provided
    [DataType(DataType.DateTime)] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid? AddedBy { get; set; }
    
    public ICollection<FoodIngredient>? FoodIngredients { get; set; }
    public ICollection<IngredientNutrient>? IngredientNutrients { get; set; }
}