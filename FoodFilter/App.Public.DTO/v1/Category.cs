using System.ComponentModel.DataAnnotations;

namespace App.Public.DTO.v1;

public class Category
{
    public Guid? Id { get; set; }

    [MaxLength(64)]
    public string Name { get; set; } = default!;
    
    public ICollection<Food>? Foods { get; set; }
}