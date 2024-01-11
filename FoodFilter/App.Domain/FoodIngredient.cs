using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.Domain;

public class FoodIngredient : DomainEntityId
{
    public Guid UnitId { get; set; }
    public Unit? Unit { get; set; }
    
    public Guid FoodId { get; set; }
    public Food? Food { get; set; }
    
    public Guid IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }
    
    public decimal Amount { get; set; }
    
    [DataType(DataType.DateTime)] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}