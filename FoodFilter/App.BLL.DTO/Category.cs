using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.BLL.DTO;

public class Category : DomainEntityId
{
    [MaxLength(64)]
    public string Name { get; set; } = default!;
    
    public ICollection<Food>? Foods { get; set; }
}