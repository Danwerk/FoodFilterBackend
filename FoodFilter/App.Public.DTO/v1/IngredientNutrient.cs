namespace App.Public.DTO.v1;

public class IngredientNutrient
{
    public Guid? Id { get; set; }

    public Guid UnitId { get; set; }
    
    public Guid IngredientId { get; set; }
    
    public Guid NutrientId { get; set; }
    public string IngredientName { get; set; } = default!;
    public string NutrientName { get; set; } = default!;
    public string UnitName { get; set; } = default!;
    public decimal Amount { get; set; }
}