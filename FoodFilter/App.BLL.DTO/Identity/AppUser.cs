using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.BLL.DTO.Identity;

public class AppUser : DomainEntityId
{
    public string Email { get; set; } = default!;
    [MinLength(5)] [MaxLength(128)]
    public string UserName { get; set; } = default!;
    
    public bool IsApproved { get; set; }
    
    public bool IsRejected { get; set; }
    
    public ICollection<AppUserRole>? AppUserRoles { get; set; }
}