namespace App.Public.DTO.v1;

public class FoodAllergen
{
    public Guid FoodId { get; set; }
    public Guid AllergenId { get; set; }
    public string? AllergenName { get; set; }
}