namespace App.Public.DTO.v1;

public class IngredientNutrient
{
    public Ingredient? Ingredient { get; set; }
    public Dictionary<string, decimal>? Nutrients { get; set; }
}