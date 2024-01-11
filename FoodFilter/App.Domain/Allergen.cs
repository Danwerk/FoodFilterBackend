using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.Domain;

public class Allergen : DomainEntityId
{
    public Guid IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }
    
    [DataType(DataType.DateTime)] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<FoodAllergen>? FoodAllergens { get; set; }

}