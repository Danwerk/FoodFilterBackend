using Domain.Base;

namespace App.BLL.DTO;

public class RestaurantAllergen : DomainEntityId
{
    public Guid RestaurantId { get; set; }
    public Guid AllergenId { get; set; }
    public Allergen? Allergen { get; set; }
}