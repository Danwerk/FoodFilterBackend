namespace App.Public.DTO.v1;

public class FoodIngredient
{
    public Guid? Id { get; set; }
    public Guid UnitId { get; set; }
    
    public Guid FoodId { get; set; }
    
    public Guid IngredientId { get; set; }
    
    public decimal Amount { get; set; }
}