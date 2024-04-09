using App.BLL.DTO;
using App.Domain.Identity;
using AutoMapper;

namespace App.Public.DTO;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.BLL.DTO.Identity.AppUser, App.Public.DTO.v1.User>().ReverseMap();
        CreateMap<App.BLL.DTO.Restaurant, App.Public.DTO.v1.Restaurant>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images!.OrderByDescending(img => img.CreatedAt).FirstOrDefault()))
            .ReverseMap();
        CreateMap<App.BLL.DTO.Food, App.Public.DTO.v1.Food>().ReverseMap();
        CreateMap<App.BLL.DTO.OpenHours, App.Public.DTO.v1.OpenHours>().ReverseMap();
        CreateMap<App.BLL.DTO.Allergen, App.Public.DTO.v1.Allergen>().ReverseMap();
        CreateMap<App.BLL.DTO.Image, App.Public.DTO.v1.Image>().ReverseMap();
        CreateMap<App.BLL.DTO.Nutrient, App.Public.DTO.v1.Nutrient>().ReverseMap();

        CreateMap<App.BLL.DTO.FoodNutrient, App.Public.DTO.v1.FoodNutrient>()
            .ForMember(dest => dest.NutrientName, opt => opt.MapFrom(src => src.Nutrient!.Name));
        CreateMap<App.Public.DTO.v1.FoodNutrient, App.BLL.DTO.FoodNutrient>()
            .ForMember(dest => dest.Nutrient, opt => opt.Ignore());

        CreateMap<App.BLL.DTO.FoodIngredient, App.Public.DTO.v1.FoodIngredient>();
            // .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient!.Name));
        CreateMap<App.Public.DTO.v1.FoodIngredient, App.BLL.DTO.FoodIngredient>();
            // .ForMember(dest => dest.Ingredient, opt => opt.Ignore());

        CreateMap<App.BLL.DTO.FoodAllergen, App.Public.DTO.v1.FoodAllergen>()
            .ForMember(dest => dest.AllergenName, opt => opt.MapFrom(src => src.Allergen!.Name));
        CreateMap<App.Public.DTO.v1.FoodAllergen, App.BLL.DTO.FoodAllergen>()
            .ForMember(dest => dest.Allergen, opt => opt.Ignore());

        CreateMap<App.BLL.DTO.Unit, App.Public.DTO.v1.Unit>().ReverseMap();
        CreateMap<App.BLL.DTO.Ingredient, App.Public.DTO.v1.Ingredient>().ReverseMap();
        CreateMap<App.BLL.DTO.IngredientNutrient, App.Public.DTO.v1.IngredientNutrient>()
            // .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient!.Name))
            // .ForMember(dest => dest.NutrientName, opt => opt.MapFrom(src => src.Nutrient!.Name))
            // .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.Unit!.UnitName))
            .ReverseMap();
    }
}