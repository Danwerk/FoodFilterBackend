using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.Domain;

public class Nutrient : DomainEntityId
{
    [MaxLength(256)]
    public string Name { get; set; } = default!;
    
    [DataType(DataType.DateTime)] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<FoodNutrient>? FoodNutrients { get; set; }

}