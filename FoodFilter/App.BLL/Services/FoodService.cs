﻿using App.BLL.DTO;
using App.Common;
using App.Common.NutrientCalculationDtos;
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

    public FoodService(IAppUOW uow, IMapper<Food, Domain.Food> mapper, IImageService imageService,
        IUnitService unitService)
        : base(uow.FoodRepository, mapper)
    {
        Uow = uow;
        _imageService = imageService;
        _unitService = unitService;
    }


    public async Task AddFoodWithImagesAsync(Food foodBll, List<IFormFile> images)
    {
        if (images.Count == 0)
        {
            await SaveFoodAsync(foodBll, new List<string>());
            return;
        }
        
        List<string> imagePaths = await _imageService.SaveImagesToFileSystemAsync(images);
        await SaveFoodAsync(foodBll, imagePaths);
    }

    private async Task SaveFoodAsync(Food foodBll, List<string> imagePaths)
    {
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

    public async Task<Food?> FindAsync(Guid foodId)
    {
        var food = await Uow.FoodRepository.FindAsync(foodId);
        return Mapper.Map(food);
    }

    public async Task<IEnumerable<Food>> AllAsync(Guid restaurantId)
    {
        var foods = await Uow.FoodRepository.AllAsync(restaurantId);

        var foodDtos = foods.Select(r => Mapper.Map(r)).ToList();

        return foodDtos!;
    }

    public async Task<List<Food>?> GetFoods()
    {
        var foods = await Uow.FoodRepository.AllAsync();

        var foodDtos = foods.Select(r => Mapper.Map(r)).ToList();

        return foodDtos!;
    }

    // public async Task<Food> Edit(Food entity)
    // {
    //     var editedFood = await Uow.FoodRepository.Edit(Mapper.Map(entity)!);
    //     return Mapper.Map(editedFood)!;
    // }


    public async Task<FoodCalculationResultDto> CalculateNutrients(FoodCalculationRequestDto request)
    {
        if (request.FoodIngredients == null)
        {
            throw new Exception($"Missing foodIngredients");
        }

        var res = new FoodCalculationResultDto();

        // Extract all IngredientIds from FoodIngredients
        var ingredientIds = request.FoodIngredients.Select(fi => fi.IngredientId).ToList();

        var ingredientNutrients = Uow.IngredientRepository.GetNutrientsForIngredients(ingredientIds);

        var nutrientGroups = ingredientNutrients
            .GroupBy(i => i.NutrientId)
            .ToList();

        foreach (var nutrientGroup in nutrientGroups)
        {
            // calculating food nutrient per food weight. Round result two decimal places.
            var calculatedNutrient = CalculateNutrientForGroup(request.FoodIngredients, nutrientGroup);

            var ingredientNames = Uow.IngredientRepository.GetIngredientNames(ingredientIds);

            // food total weight
            res.ServingInGrams = request.FoodIngredients.Sum(i => i.Amount);

            // set ingredients to result 
            res.Ingredients = ingredientNames.Select(name => new IngredientDto { Name = name }).ToList();
            res.Nutrients.Add(calculatedNutrient);
        }

        // Calculate food calories, per 100 grams and per food total weight
        var foodTotalCaloriesPerFoodTotalWeight = await CalculateTotalCalories(request.FoodIngredients, ingredientIds, ingredientNutrients);
        var foodTotalCaloriesPer100Grams = Math.Round(foodTotalCaloriesPerFoodTotalWeight / res.ServingInGrams * 100, 1);

        res.KCaloriesPerFoodTotalWeight = foodTotalCaloriesPerFoodTotalWeight;
        res.KCaloriesPer100Grams = foodTotalCaloriesPer100Grams;
        return res;
    }

    private NutrientDto CalculateNutrientForGroup(List<FoodIngredientDto> foodIngredients,
        IGrouping<Guid, IngredientNutrientDto> nutrientGroup)
    {
        // calculating food nutrient per food weight. Round result two decimal places.
        var amountPerFoodTotalWeight = nutrientGroup.Sum(i =>
            _unitService.ConvertToGrams(i.Amount * foodIngredients
                .First(fi => fi.IngredientId == i.IngredientId).Amount, i.UnitName)) / 100;

        var amountPerFoodTotalWeightRounded = Math.Round(amountPerFoodTotalWeight, 1);

        var foodTotalWeight = foodIngredients.Sum(i => i.Amount);

        var nutrientName = nutrientGroup.First().NutrientName;
        var unitName = nutrientGroup.First().UnitName;

        return new NutrientDto
        {
            Name = nutrientName,
            AmountPerFoodTotalWeight = amountPerFoodTotalWeightRounded,
            AmountPer100Grams = Math.Round(amountPerFoodTotalWeight / foodTotalWeight * 100, 1),
            UnitName = unitName,
            NutrientId = nutrientGroup.Key,
            UnitId = nutrientGroup.First().UnitId
        };
    }


    private async Task<decimal> CalculateTotalCalories(List<FoodIngredientDto> foodIngredients, List<Guid> ingredientIds,
        List<IngredientNutrientDto> ingredientNutrients)
    {
        decimal totalCalories = 0;
        var ingredients = await Uow.IngredientRepository.GetIngredientsByIdsAsync(ingredientIds);

        foreach (var id in ingredientIds)
        {
            var ingredient = foodIngredients.FirstOrDefault(f => f.IngredientId == id);
            if (ingredient != null)
            {
                var ingredientInfo = ingredients.FirstOrDefault(i => i.Id == id);

                if (ingredientInfo != null)
                {
                    decimal caloriesPer100Grams = ingredientInfo.KCaloriesPer100Grams;
                    decimal amount = ingredient.Amount;
                    totalCalories += (amount * caloriesPer100Grams);
                }
            }
        }

        return Math.Round(totalCalories / 100, 1);
    }
}