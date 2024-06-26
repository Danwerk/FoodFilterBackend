﻿using Domain.Contracts.Base;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppRole :  IdentityRole<Guid>, IDomainEntityId
{
    public ICollection<AppUserRole>? AppUserRoles { get; set; }

}