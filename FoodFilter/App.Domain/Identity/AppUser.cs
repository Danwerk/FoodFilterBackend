using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Contracts.Base;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser :  IdentityUser<Guid>, IDomainEntityId
{
    // [MinLength(1)]
    // [MaxLength(128)]
    // public string FirstName { get; set; } = default!;
    //
    // [MinLength(1)]
    // [MaxLength(128)]
    // public string LastName { get; set; } = default!;

    public bool IsApproved { get; set; }
    
    // public int? EarnedPoints { get; set; }
    //
    // public int? TrustPoints { get; set; }
    
    [InverseProperty(nameof(Restaurant.ApprovedBy))]
    public ICollection<Restaurant>? approvedRestaurants { get; set; }
    public ICollection<AppRefreshToken>? AppRefreshTokens { get; set; }

    public ICollection<Restaurant>? Restaurants { get; set; }
    
    public ICollection<SubAdmin>? SubAdmins { get; set; }

    
}