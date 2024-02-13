namespace App.Common.NutrientCalculationDtos;

public class FoodCalculationResultDto
{
    public decimal ServingInGrams { get; set; }
    public decimal KCaloriesPerFoodTotalWeight { get; set; }
    public decimal KCaloriesPer100Grams { get; set; }
    public List<NutrientDto> Nutrients { get; set; } = new List<NutrientDto>();
    public List<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();
}