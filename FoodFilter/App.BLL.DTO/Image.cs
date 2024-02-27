using Domain.Base;

namespace App.BLL.DTO;

public class Image : DomainEntityId
{
    public Guid RestaurantId { get; set; }

    public string? Url { get; set; }
    
    public bool IsMain { get; set; }
    
    public bool IsApproved { get; set; }
    
    public DateTime CreatedAt { get; set; }
}