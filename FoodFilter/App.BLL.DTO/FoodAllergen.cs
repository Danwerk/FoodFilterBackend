using Domain.Base;

namespace App.BLL.DTO;

public class FoodAllergen : DomainEntityId
{
    public Guid FoodId { get; set; }
    public Guid AllergenId { get; set; }
}