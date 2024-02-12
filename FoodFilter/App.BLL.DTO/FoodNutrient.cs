using Domain.Base;

namespace App.BLL.DTO;

public class FoodNutrient : DomainEntityId
{
    public Guid NutrientId { get; set; }
    public string NutrientName { get; set; } = default!;
    public decimal AmountPer100Grams { get; set; }
    public decimal AmountPerFoodTotalWeight { get; set; }
    public string UnitName { get; set; } = default!;



}