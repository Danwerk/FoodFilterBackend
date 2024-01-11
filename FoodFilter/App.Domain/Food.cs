using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.Domain;

public class Food : DomainEntityId
{
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public Guid RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
    
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    [MaxLength(512)]
    public string Description { get; set; } = default!;
    
    public decimal Price { get; set; }
    
    [MaxLength(512)] 
    public string ImageUrl { get; set; } = default!;
    
    public byte[]? ImageData { get; set; }
    
    [DataType(DataType.DateTime)] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<FoodNutrient>? FoodNutrients { get; set; }
    public ICollection<FoodIngredient>? FoodIngredients { get; set; }
    public ICollection<FoodAllergen>? FoodAllergens { get; set; }


}