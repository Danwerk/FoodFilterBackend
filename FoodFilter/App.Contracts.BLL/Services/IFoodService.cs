using App.BLL.DTO;
using App.Common.NutrientCalculationDtos;
using Base.Contracts.DAL;
using Microsoft.AspNetCore.Http;

namespace App.Contracts.BLL.Services;

public interface IFoodService : IBaseRepository<Food>
{
    Task AddFoodWithImagesAsync(Food food, List<IFormFile> images);
    new Task<Food?> FindAsync(Guid foodId);
    Task<IEnumerable<Food>> AllAsync(Guid restaurantId);
    Task<IEnumerable<Food>> PublishedAllAsync(Guid restaurantId);
    Task<List<Food>?> GetFoods();
    Task<Food?> PublishFoodAsync(Guid id);
    Task<Food?> UnpublishFoodAsync(Guid id);
    // Task<Food> Edit(Food entity);
    
    Task<FoodCalculationResultDto> CalculateNutrients(FoodCalculationRequestDto request);


}