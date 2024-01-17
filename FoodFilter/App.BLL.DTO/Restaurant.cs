using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.BLL.DTO;

public class Restaurant : DomainEntityId
{
    public string Name { get; set; } = default!;
    public string FullAddress { get; set; } = default!;
    public string OpenHours { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string WebSiteUrl { get; set; } = default!;
    public Guid AppUserId { get; set; }
}