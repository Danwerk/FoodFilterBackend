using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.Domain;

public class FoodClaim : DomainEntityId
{
    public Guid FoodId { get; set; }
    public Food? Food { get; set; }
    
    public Guid ClaimId { get; set; }
    public Claim? Claim { get; set; }

    [DataType(DataType.DateTime)] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}