using App.Domain;
using Domain.Base;

namespace App.BLL.DTO;

using System.ComponentModel.DataAnnotations;
public class Food : DomainEntityId
{
    public Guid RestaurantId { get; set; }
    
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    [MaxLength(512)]
    public string Description { get; set; } = default!;
    
    public decimal Price { get; set; }

    public bool IsActive { get; set; }

    public ICollection<FoodNutrient>? FoodNutrients { get; set; }
    public ICollection<FoodIngredient>? FoodIngredients { get; set; }
    public ICollection<FoodAllergen>? FoodAllergens { get; set; }
    public ICollection<FoodImage>? FoodImages { get; set; }


}