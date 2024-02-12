namespace App.Public.DTO.v1;

public class FoodNutrient
{
    public Guid? Id { get; set; }
    public string NutrientName { get; set; } = default!;
    //public decimal Amount { get; set; }
    public decimal AmountPer100Grams { get; set; }
    public decimal AmountPerFoodTotalWeight { get; set; }
    public string UnitName { get; set; } = default!;
}
