using System.ComponentModel.DataAnnotations;

namespace App.Public.DTO.v1;

public class Food
{
    public Guid? Id { get; set; }
   
    public Guid? CategoryId { get; set; }
    
    public Guid? RestaurantId { get; set; }
    
    [MaxLength(256)]
    public string? Name { get; set; } = default!;

    [MaxLength(512)]
    public string? Description { get; set; } = default!;
    
    public decimal? Price { get; set; }

    public bool? IsActive { get; set; }
    
}