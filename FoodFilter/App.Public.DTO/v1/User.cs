using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.Public.DTO.v1;

public class User 
{
    public Guid Id { get; set; }
    
    [MinLength(5)] [MaxLength(128)]
    public string Email { get; set; } = default!;

    [MinLength(1)] [MaxLength(255)]
    public string UserName { get; set; } = default!;
    
    public bool IsApproved { get; set; }
    
    public bool IsRejected { get; set; }
    
    public Guid? RestaurantId { get; set; }
}