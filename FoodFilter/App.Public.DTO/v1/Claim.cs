using System.ComponentModel.DataAnnotations;

namespace App.Public.DTO.v1;

public class Claim
{
    public Guid Id { get; set; }
    [MaxLength(256)]
    public string Name { get; set; } = default!;
}