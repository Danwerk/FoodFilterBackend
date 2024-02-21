namespace App.Public.DTO.v1;

public class FoodNutrient
{
    public Guid? Id { get; set; }
    public Guid FoodId { get; set; }
    public Guid NutrientId { get; set; }
    public Guid UnitId { get; set; }
    public string? NutrientName { get; set; }
    
    public decimal AmountPer100Grams { get; set; }
    public decimal AmountPerFoodTotalWeight { get; set; }
    //public string UnitName { get; set; } = default!;
}
