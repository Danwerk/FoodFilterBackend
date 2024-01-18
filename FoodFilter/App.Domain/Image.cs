using Domain.Base;

namespace App.Domain;

public class Image : DomainEntityId
{
    public Guid RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
    
    public string? Url { get; set; }
    
    public bool IsMain { get; set; }
    
    public bool IsApproved { get; set; }
    
}