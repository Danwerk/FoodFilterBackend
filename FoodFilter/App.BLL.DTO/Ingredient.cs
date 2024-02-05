﻿using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace App.BLL.DTO;

public class Ingredient: DomainEntityId
{
    public string Name { get; set; } = default!;

    [MaxLength(512)]
    public string Description { get; set; } = default!;
    
    public bool IsConfirmed { get; set; }
}