using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.Domain;

public class OpenHours : DomainEntityId
{
    public Guid RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }

    [MaxLength(32)]
    public string Weekday { get; set; } = default!;
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    [DataType(DataType.DateTime)] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}