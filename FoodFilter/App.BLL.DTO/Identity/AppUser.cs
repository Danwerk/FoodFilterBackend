using Domain.Base;

namespace App.BLL.DTO.Identity;

public class AppUser : DomainEntityId
{
    public string UserName { get; set; } = default!;
    
    public bool IsApproved { get; set; }
    
    public ICollection<AppUserRole>? AppUserRoles { get; set; }
}