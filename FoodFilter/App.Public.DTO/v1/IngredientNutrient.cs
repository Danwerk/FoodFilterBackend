namespace App.Public.DTO.v1;

public class IngredientNutrient
{
    public Guid? Id { get; set; }

    public Guid UnitId { get; set; }
    
    public Guid IngredientId { get; set; }
    
    public Guid NutrientId { get; set; }
    
    public decimal Amount { get; set; }
}