using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.Public.DTO.v1;

public class OpenHours
{
    public Guid Id { get; set; }
    
    public Guid RestaurantId { get; set; }
    
    [MaxLength(32)]
    public string Day { get; set; } = default!;
    
    [DataType(DataType.Time)]
    public TimeSpan Open { get; set; }     
    
    [DataType(DataType.Time)]
    public TimeSpan Close { get; set; }
}