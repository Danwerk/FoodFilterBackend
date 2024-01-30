﻿using App.BLL.DTO;
using Base.Contracts.DAL;
using Microsoft.AspNetCore.Http;

namespace App.Contracts.BLL.Services;

public interface IFoodService : IBaseRepository<Food>
{
    Task AddFoodWithImagesAsync(Food food, List<IFormFile> images);
}