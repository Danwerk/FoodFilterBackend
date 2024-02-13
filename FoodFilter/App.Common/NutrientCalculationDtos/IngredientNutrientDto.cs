namespace App.Common.NutrientCalculationDtos;

public class IngredientNutrientDto
{
    public string NutrientName { get; set; } = default!;
    public Guid NutrientId { get; set; }
    public Guid IngredientId { get; set; }
    public decimal Amount { get; set; }

    public string UnitName { get; set; } = default!;
}