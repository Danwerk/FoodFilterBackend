namespace App.Public.DTO.v1;

public class FoodNutritionCalculation
{
    public string FoodName { get; set; } = default!;
    
    public List<FoodNutrient>? FoodNutrients { get; set; }
}