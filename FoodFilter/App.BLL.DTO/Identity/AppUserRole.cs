using Domain.Base;

namespace App.BLL.DTO.Identity;

public class AppUserRole : DomainEntityId
{
    public Guid AppUserId { get; set; }
    
    public Guid AppRoleId { get; set; }
    public AppRole? AppRole { get; set; }
}