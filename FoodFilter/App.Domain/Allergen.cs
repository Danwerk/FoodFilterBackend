using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.Domain;

public class Allergen : DomainEntityId
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    [DataType(DataType.DateTime)] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<FoodAllergen>? FoodAllergens { get; set; }

}