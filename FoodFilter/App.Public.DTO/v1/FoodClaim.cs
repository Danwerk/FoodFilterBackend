namespace App.Public.DTO.v1;

public class FoodClaim
{
    public Guid FoodId { get; set; }
    public Guid ClaimId { get; set; }
    public string? ClaimName { get; set; }
}