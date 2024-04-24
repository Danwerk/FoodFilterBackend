using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace App.Public.DTO.v1;

public class Food
{
    public Guid? Id { get; set; }
    
    public Guid? RestaurantId { get; set; }
    
    [MaxLength(256)]
    public string? Name { get; set; } = default!;

    [MaxLength(512)]
    public string? Description { get; set; } = default!;
    
    public decimal? Price { get; set; }

    public bool? IsPublished { get; set; }
    
    public decimal ServingInGrams { get; set; }
    
    public decimal? KCaloriesPerFoodTotalWeight { get; set; }
    
    public decimal? KCaloriesPer100Grams { get; set; }
    
    public ICollection<FoodNutrient>? FoodNutrients { get; set; }

    public ICollection<FoodIngredient>? FoodIngredients { get; set; }
    public ICollection<FoodAllergen>? FoodAllergens { get; set; }
    public ICollection<FoodClaim>? FoodClaims { get; set; }
    public ICollection<Image>? Images { get; set; }

    
    
}