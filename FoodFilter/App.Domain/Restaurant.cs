using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Domain.Base;

namespace App.Domain;

public class Restaurant : DomainEntityId
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    [MaxLength(256)]
    public string Name { get; set; } = default!;
    
    [MaxLength(512)] 
    public string ImageUrl { get; set; } = default!;
    
    public byte[]? ImageData { get; set; }

    [MaxLength(128)]
    public string Address { get; set; } = default!;

    [MaxLength(32)]
    public string PhoneNumber { get; set; } = default!;

    [MaxLength(512)]
    public string Website { get; set; } = default!;
    
    
    
    public Guid ApprovedById { get; set; }
    public AppUser? ApprovedBy { get; set; }
    
    [DataType(DataType.DateTime)] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<Food>? Foods { get; set; }
    public ICollection<OpenHours>? OpenHours { get; set; }

}