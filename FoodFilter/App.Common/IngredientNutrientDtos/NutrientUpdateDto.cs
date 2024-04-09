namespace App.Common.IngredientNutrientDtos;

public class NutrientUpdateDto
{
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; }
}