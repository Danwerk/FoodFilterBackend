using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.Domain;

public class FoodNutrient : DomainEntityId
{
    public Guid UnitId { get; set; }
    public Unit? Unit { get; set; }
    
    public Guid FoodId { get; set; }
    public Food? Food { get; set; }
    
    public Guid NutrientId { get; set; }
    public Nutrient? Nutrient { get; set; }
    
    public decimal? AmountPer100Grams { get; set; }
    public decimal? AmountPerFoodTotalWeight { get; set; }
    
    [DataType(DataType.DateTime)] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}