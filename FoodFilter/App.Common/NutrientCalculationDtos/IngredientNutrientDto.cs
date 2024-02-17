namespace App.Common.NutrientCalculationDtos;

public class IngredientNutrientDto
{
    public string NutrientName { get; set; } = default!;
    public Guid NutrientId { get; set; }
    public Guid IngredientId { get; set; }
    public decimal Amount { get; set; }
    public Guid? UnitId { get; set; } = default!;
    public string UnitName { get; set; } = default!;
}