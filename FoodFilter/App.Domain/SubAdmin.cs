using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Domain.Base;

namespace App.Domain;

public class SubAdmin : DomainEntityId
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    [MaxLength(128)]
    public string Region { get; set; } = default!;

    [MaxLength(256)]
    public string Languages { get; set; } = default!;
    
    public int Productivity { get; set; }
}