using Domain.Base;

namespace App.BLL.DTO.Identity;

public class AppRole : DomainEntityId
{
    public string Name { get; set; } = default!;
    
    public ICollection<AppUserRole>? AppUserRoles { get; set; }

}