using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.BLL.DTO;

public class OpenHours : DomainEntityId
{
    public Guid RestaurantId { get; set; }
    
    [MaxLength(32)]
    public string Day { get; set; } = default!;
    
    [DataType(DataType.Time)]
    public TimeSpan Open { get; set; }     
    
    [DataType(DataType.Time)]
    public TimeSpan Close { get; set; }
}