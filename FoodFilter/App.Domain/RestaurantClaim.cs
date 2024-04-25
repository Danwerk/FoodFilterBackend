using Domain.Base;

namespace App.Domain;

public class RestaurantClaim : DomainEntityId
{
    public Guid RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
    public Guid ClaimId { get; set; }
    public Claim? Claim { get; set; }
}