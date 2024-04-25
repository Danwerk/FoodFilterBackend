using App.Contracts.BLL.Services;
using App.Contracts.BLL.Services.Identity;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBLL : IBaseBLL
{
    IUnitService UnitService { get; }
    IRestaurantService RestaurantService { get; }
    IFoodService FoodService { get; }
    IIngredientService IngredientService { get; }
    IAllergenService AllergenService { get; }
    INutrientService NutrientService { get; }
    IIngredientNutrientService IngredientNutrientService { get; }
    IFileService FileService { get; }
    IImageService ImageService { get; }
    IOpenHoursService OpenHoursService { get; }
    IRestaurantAllergenService RestaurantAllergenService { get; }
    IClaimService ClaimService { get; }
    IRestaurantClaimService RestaurantClaimService { get; }
    // IUserService UserService { get; }
}