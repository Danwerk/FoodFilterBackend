using App.BLL.DTO;
using App.Common;
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

public class FoodService : BaseEntityService<Food, App.Domain.Food, IFoodRepository>, IFoodService
{
    protected IAppUOW Uow;
    private readonly IImageService _imageService;
    private readonly IUnitService _unitService;

    public FoodService(IAppUOW uow, IMapper<Food, Domain.Food> mapper, IImageService imageService, IUnitService unitService)
        : base(uow.FoodRepository, mapper)
    {
        Uow = uow;
        _imageService = imageService;
        _unitService = unitService;
    }


    public async Task AddFoodWithImagesAsync(Food foodBll, List<IFormFile> images)
    {
        List<string> imagePaths = await _imageService.SaveImagesToFileSystemAsync(images);

        var food = Mapper.Map(foodBll);
        if (food != null)
        {
            food.Description = "";
        }

        var savedFood = Uow.FoodRepository.Add(food!);
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

        var foodDtos = foods.Select(r => Mapper.Map(r)).ToList();

        return foodDtos;
    }

    // public async Task<Food> Edit(Food entity)
    // {
    //     var editedFood = await Uow.FoodRepository.Edit(Mapper.Map(entity)!);
    //     return Mapper.Map(editedFood)!;
    // }

    public Task<FoodNutritionCalculation> CalculateFoodNutrition(Food food)
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
        
        // Grouping nutrients by their types to calculate total.
        var nutrientGroups = food.FoodIngredients
            .SelectMany(fi => fi.Ingredient!.IngredientNutrients!)
            .GroupBy(i => i.NutrientId)
            .ToList();

        foreach (var nutrientGroup in nutrientGroups)
        {
            // calculating food nutrient per food weight. Round result two decimal places.
            var amountPerFoodTotalWeight = nutrientGroup.Sum(i =>
                _unitService.ConvertToGrams(i.Amount * food.FoodIngredients
                    .First(fi => fi.IngredientId == i.IngredientId).Amount, i.Unit!.UnitName)) / 100;

            var amountPerFoodTotalWeightRounded = Math.Round(amountPerFoodTotalWeight, 2);

            // calculating food nutrient per 100 grams. Round result two decimal places.
            var foodTotalWeight = food.FoodIngredients.Sum(i => i.Amount);
            var amountPer100Grams = amountPerFoodTotalWeight / foodTotalWeight * 100;
            var amountPer100GramsRounded = Math.Round(amountPer100Grams, 2);
            
            var calculatedNutrient = new FoodNutrient()
            {
                Id = nutrientGroup.Key,
                AmountPerFoodTotalWeight = amountPerFoodTotalWeightRounded,
                AmountPer100Grams = amountPer100GramsRounded,
                NutrientName = nutrientGroup.First()!.Nutrient!.Name,
                NutrientId = nutrientGroup.First().Nutrient!.Id,
                UnitName = UnitTypes.G
            };
            foodNutrition.FoodNutrients.Add(calculatedNutrient);
        }
        
        return Task.FromResult(foodNutrition);
    }
}