using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Domain;
using Base.BLL;
using Base.Contracts;
using Microsoft.AspNetCore.Http;
using Food = App.BLL.DTO.Food;

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
}