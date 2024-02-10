namespace App.BLL.DTO;

public class FoodNutritionCalculation
{
    public string FoodName { get; set; } = default!;
    
    public List<FoodNutrient>? FoodNutrients { get; set; }
}