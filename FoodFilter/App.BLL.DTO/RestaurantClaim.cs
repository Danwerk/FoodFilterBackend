using Domain.Base;

namespace App.BLL.DTO;

public class RestaurantClaim : DomainEntityId
{
    public Guid RestaurantId { get; set; }
    public Guid ClaimId { get; set; }
    public Claim? Claim { get; set; }
}