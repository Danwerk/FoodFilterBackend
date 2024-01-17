using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUserRole : IdentityUserRole<Guid>
{
    public AppUser? AppUser { get; set; }
    public AppRole? AppRole { get; set; }
}