using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.Domain;

public class FoodAllergen : DomainEntityId
{
    public Guid FoodId { get; set; }
    public Food? Food { get; set; }
    
    public Guid AllergenId { get; set; }
    public Allergen? Allergen { get; set; }

    [DataType(DataType.DateTime)] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}