namespace App.Common.IngredientNutrientDtos;

public class IngredientNutrientUpdateDto
{
    public Guid IngredientId { get; set; }
    
    public List<NutrientUpdateDto>? Nutrients { get; set; }
}