using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.Domain;

public class Ingredient : DomainEntityId
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;

    [MaxLength(512)]
    public string Description { get; set; } = default!;
    
    public bool IsConfirmed { get; set; }

    // Declaring default values for createdAt and updatedAt, if values are not provided
    [DataType(DataType.DateTime)] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<FoodIngredient>? FoodIngredients { get; set; }
    public ICollection<Allergen>? Allergens { get; set; }

}