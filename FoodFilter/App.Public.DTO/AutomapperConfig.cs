using App.Domain.Identity;
using AutoMapper;

namespace App.Public.DTO;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.BLL.DTO.Identity.AppUser, App.Public.DTO.v1.User>().ReverseMap();
        CreateMap<App.BLL.DTO.Restaurant, App.Public.DTO.v1.Restaurant>().ReverseMap();
        CreateMap<App.BLL.DTO.Food, App.Public.DTO.v1.Food>().ReverseMap();
        CreateMap<App.BLL.DTO.FoodIngredient, App.Public.DTO.v1.FoodIngredient>().ReverseMap();
        CreateMap<App.BLL.DTO.Unit, App.Public.DTO.v1.Unit>().ReverseMap();
        CreateMap<App.BLL.DTO.Ingredient, App.Public.DTO.v1.Ingredient>().ReverseMap();
    }
}