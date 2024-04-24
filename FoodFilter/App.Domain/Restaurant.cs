using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Domain.Base;

namespace App.Domain;

public class Restaurant : DomainEntityId
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    [MaxLength(256)]
    public string? Name { get; set; }

    [MaxLength(128)]
    public string? City { get; set; }
    [MaxLength(128)]
    public string? Street { get; set; }
    [MaxLength(128)]
    public string? StreetNumber { get; set; }

    [MaxLength(32)]
    public string? PhoneNumber { get; set; }

    [MaxLength(512)]
    public string? Website { get; set; } 
    
    public Guid? ApprovedById { get; set; }
    public AppUser? ApprovedBy { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    
    public DateTime? PaymentStartsAt { get; set; }

    
    public DateTime? PaymentEndsAt { get; set; }

    public bool IsSubscriptionExpired
    {
        get
        {
            return PaymentEndsAt.HasValue && PaymentEndsAt.Value < DateTime.UtcNow;
        }
    }
    
    public ICollection<Food>? Foods { get; set; }
    public ICollection<Image>? Images { get; set; }
    
    public ICollection<OpenHours>? OpenHours { get; set; }
    public ICollection<RestaurantAllergen>? RestaurantAllergens { get; set; }

}