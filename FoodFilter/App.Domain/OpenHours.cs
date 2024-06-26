﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace App.Domain;

public class OpenHours : DomainEntityId
{
    public Guid RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }

    [MaxLength(32)]
    public string Day { get; set; } = default!;
    
    [DataType(DataType.Time)]
    public TimeSpan Open { get; set; }     
    
    [DataType(DataType.Time)]
    public TimeSpan Close { get; set; }
    
    [DataType(DataType.DateTime)] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}