using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.BLL.DTO;

public class Restaurant : DomainEntityId
{
    public string Name { get; set; } = default!;
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? StreetNumber { get; set; }
    
    public string OpenHours { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string? Website { get; set; }
    
    public Guid AppUserId { get; set; }

    public string Email { get; set; } = default!;
    
    public bool IsApproved { get; set; }
    public bool IsRejected { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime? PaymentStartsAt { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? PaymentEndsAt { get; set; }

    public bool IsSubscriptionExpired { get; set; }

    public List<Image>? Images { get; set; }

}