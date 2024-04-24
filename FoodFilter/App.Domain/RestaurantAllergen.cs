using Domain.Base;

namespace App.Domain;

public class RestaurantAllergen : DomainEntityId
{
    public Guid RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
    
    public Guid AllergenId { get; set; }
    public Allergen? Allergen { get; set; }
}