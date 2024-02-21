using App.Contracts.DAL.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUOW : IBaseUOW
{
    // List your repositories here
    IAllergenRepository AllergenRepository { get; }
    IFoodAllergenRepository FoodAllergenRepository { get; }
    IFoodIngredientRepository FoodIngredientRepository { get; }
    IFoodNutrientRepository FoodNutrientRepository { get; }
    IFoodRepository FoodRepository { get; }
    IIngredientRepository IngredientRepository { get; }
    IIngredientNutrientRepository IngredientNutrientRepository { get; }
    INutrientRepository NutrientRepository { get; }
    IOpenHoursRepository OpenHoursRepository { get; }
    IRestaurantRepository RestaurantRepository { get; }
    ISubAdminRepository SubAdminRepository { get; }
    IUnitRepository UnitRepository { get; }
    IUserRepository UserRepository { get; }
    IImageRepository ImageRepository { get; }

}