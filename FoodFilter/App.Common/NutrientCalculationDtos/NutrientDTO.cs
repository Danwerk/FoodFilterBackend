namespace App.Common.NutrientCalculationDtos;

public class NutrientDto
{
    public string Name { get; set; } = default!;
    public decimal AmountPer100Grams { get; set; }
    public decimal AmountPerFoodTotalWeight { get; set; }
    public string UnitName { get; set; } = default!;
}