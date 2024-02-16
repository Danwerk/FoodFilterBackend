using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.BLL.DTO;

public class Allergen : DomainEntityId
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
}