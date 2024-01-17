﻿using App.BLL.DTO.Identity;
using Base.Contracts.DAL;
using AppUser = App.Domain.Identity.AppUser;

namespace App.Contracts.DAL.Identity;

public interface IUserRepository: IBaseRepository<AppUser>
{
    public Task<IEnumerable<AppUser>> GetAllUsersWithRolesAsync();
    
}