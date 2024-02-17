using System.ComponentModel.DataAnnotations;

namespace App.Public.DTO.v1;

public class Allergen
{
    public Guid Id { get; set; }
    [MaxLength(128)]
    public string Name { get; set; } = default!;
}