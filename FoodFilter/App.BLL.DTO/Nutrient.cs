using Domain.Base;

namespace App.BLL.DTO;

public class Nutrient : DomainEntityId
{
    public string Name { get; set; } = default!;
    
    public ICollection<FoodNutrient>? FoodNutrients { get; set; }
    public ICollection<IngredientNutrient>? IngredientNutrients { get; set; }
}