namespace App.Common.NutrientCalculationDtos;

public class FoodCalculationResultDto
{
    public decimal ServingInGrams { get; set; }
    public List<NutrientDto> Nutrients { get; set; } = new List<NutrientDto>();
    public List<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();
}