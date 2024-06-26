﻿using System.ComponentModel.DataAnnotations;

namespace App.Public.DTO.v1.Identity;

public class Register
{
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Wrong length on email")]
    public string Email { get; set; } = default!;
    
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Wrong length on password")]
    public string Password { get; set; } = default!;
}