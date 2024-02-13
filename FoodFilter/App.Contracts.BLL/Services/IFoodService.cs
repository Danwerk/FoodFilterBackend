using App.BLL.DTO;
using App.Common.NutrientCalculationDtos;
using Base.Contracts.DAL;
using Microsoft.AspNetCore.Http;

namespace App.Contracts.BLL.Services;

public interface IFoodService : IBaseRepository<Food>
{
    Task AddFoodWithImagesAsync(Food food, List<IFormFile> images);
    Task<Food?> FindAsync(Guid foodId);
    Task<IEnumerable<Food>> AllAsync(Guid restaurantId);
    Task<List<Food>?> GetFoods();
    
    // Task<Food> Edit(Food entity);

    Task<FoodNutritionCalculation> CalculateFoodNutrition(Food food);

    Task<FoodCalculationResultDto> CalculateNutrients(FoodCalculationRequestDto request);


}