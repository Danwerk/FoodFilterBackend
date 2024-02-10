using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Domain;
using Base.BLL;
using Base.Contracts;
using Microsoft.AspNetCore.Http;
using Food = App.BLL.DTO.Food;
using FoodNutrient = App.BLL.DTO.FoodNutrient;
using Image = App.Domain.Image;

namespace App.BLL.Services;

public class FoodService : BaseEntityService<App.BLL.DTO.Food, App.Domain.Food, IFoodRepository>, IFoodService
{
    protected IAppUOW Uow;
    private readonly IImageService _imageService;

    public FoodService(IAppUOW uow, IMapper<Food, Domain.Food> mapper, IImageService imageService)
        : base(uow.FoodRepository, mapper)
    {
        Uow = uow;
        _imageService = imageService;
    }


    public async Task AddFoodWithImagesAsync(Food foodBll, List<IFormFile> images)
    {
        List<string> imagePaths = await _imageService.SaveImagesToFileSystemAsync(images);

        var food = Mapper.Map(foodBll);
        if (food != null && food.Description == null)
        {
            food.Description = "";
        }

        var savedFood = Uow.FoodRepository.Add(food);
        await Uow.SaveChangesAsync();

        foreach (var imagePath in imagePaths)
        {
            var foodImage = new Image
            {
                EntityType = EntityType.Food,
                Food = savedFood,
                IsApproved = false,
                IsMain = false,
                Url = imagePath,
            };
            Uow.ImageRepository.Add(foodImage);
        }

        await Uow.SaveChangesAsync();
    }

    public async Task<Food?> GetFood(Guid foodId)
    {
        var food = await Uow.FoodRepository.FindAsync(foodId);
        return Mapper.Map(food);
    }

    public async Task<List<Food>?> GetFoods()
    {
        var foods = await Uow.FoodRepository.AllAsync();

        var foodDtos = foods?.Select(r => Mapper.Map(r)).ToList();

        return foodDtos;
    }

    public async Task<FoodNutritionCalculation> CalculateFoodNutrition(Food food)
    {
        if (food.FoodIngredients == null)
        {
            throw new Exception($"Food {food.Name} is missing foodIngredients");
        }

        if (food.FoodNutrients == null)
        {
            throw new Exception($"Food {food.Name} is missing foodNutrients");
        }

        var foodNutrition = new FoodNutritionCalculation
        {
            FoodName = food.Name,
            FoodNutrients = new List<FoodNutrient>()
        };

        var nutrientGroups = food.FoodIngredients
            .SelectMany(f => f.Ingredient!.IngredientNutrients!)
            .GroupBy(inut => inut.NutrientId)
            .ToList();

        foreach (var nutrientGroup in nutrientGroups)
        {
            var totalAmount = nutrientGroup.Sum(i => i.Amount);

            var calculatedNutrient = new FoodNutrient()
            {
                Id = nutrientGroup.Key,
                Amount = totalAmount,
                NutrientName = nutrientGroup.First()!.Nutrient!.Name,
                NutrientId = nutrientGroup.First().Nutrient!.Id,
            };
            foodNutrition.FoodNutrients.Add(calculatedNutrient);
        }

        return foodNutrition;
    }
}