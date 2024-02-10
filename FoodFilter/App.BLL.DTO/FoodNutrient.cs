using Domain.Base;

namespace App.BLL.DTO;

public class FoodNutrient : DomainEntityId
{
    public Guid NutrientId { get; set; }
    public string NutrientName { get; set; } = default!;
    public decimal Amount { get; set; }
    
    
    
}