using Domain.Base;

namespace App.BLL.DTO;

public class FoodClaim : DomainEntityId
{
    public Guid FoodId { get; set; }
    public Guid ClaimId { get; set; }
    public Claim? Claim { get; set; }
}