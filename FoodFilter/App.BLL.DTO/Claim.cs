using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.BLL.DTO;

public class Claim : DomainEntityId
{
    [MaxLength(256)]
    public string Name { get; set; } = default!;
}