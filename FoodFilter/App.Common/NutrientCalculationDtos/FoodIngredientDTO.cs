namespace App.Common.NutrientCalculationDtos;

public class FoodIngredientDto
{
    public Guid IngredientId { get; set; }
    public decimal Amount { get; set; }
    public string Unit { get; set; } = default!;
}