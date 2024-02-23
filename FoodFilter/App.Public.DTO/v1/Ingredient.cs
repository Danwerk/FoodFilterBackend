using System.ComponentModel.DataAnnotations;

namespace App.Public.DTO.v1;

public class Ingredient
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    [MaxLength(512)]
    public string? Description { get; set; }
    
    public decimal? KCaloriesPer100Grams { get; set; }

    public bool IsConfirmed { get; set; }
}