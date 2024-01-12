using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using DAL.EF.Repositories;

namespace DAL.EF;

public class AppUOW : EFBaseUOW<ApplicationDbContext>, IAppUOW
{
    public AppUOW(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public IAllergenRepository? _allergenRepository;
    public ICategoryRepository? _categoryRepository;
    public IFoodAllergenRepository? _foodAllergenRepository;
    public IFoodIngredientRepository? _foodIngredientRepository;
    public IFoodNutrientRepository? _foodNutrientRepository;
    public IFoodRepository? _foodRepository;
    public IIngredientRepository? _ingredientRepository;
    public INutrientRepository? _nutrientRepository;
    public IOpenHoursRepository? _openHoursRepository;
    public IRestaurantRepository? _restaurantRepository;
    public ISubAdminRepository? _subAdminRepository;
    public IUnitRepository? _unitRepository;

    public IAllergenRepository AllergenRepository => _allergenRepository ??= new AllergenRepository(UowDbContext);
    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(UowDbContext);
    public IFoodAllergenRepository FoodAllergenRepository => _foodAllergenRepository ??= new FoodAllergenRepository(UowDbContext);
    public IFoodIngredientRepository FoodIngredientRepository => _foodIngredientRepository ??= new FoodIngredientRepository(UowDbContext);
    public IFoodNutrientRepository FoodNutrientRepository => _foodNutrientRepository ??= new FoodNutrientRepository(UowDbContext);
    public IFoodRepository FoodRepository => _foodRepository ??= new FoodRepository(UowDbContext);
    public IIngredientRepository IngredientRepository => _ingredientRepository ??= new IngredientRepository(UowDbContext);
    public INutrientRepository NutrientRepository => _nutrientRepository ??= new NutrientRepository(UowDbContext);
    public IOpenHoursRepository OpenHoursRepository => _openHoursRepository ??= new OpenHoursRepository(UowDbContext);
    public IRestaurantRepository RestaurantRepository => _restaurantRepository ??= new RestaurantRepository(UowDbContext);
    public ISubAdminRepository SubAdminRepository => _subAdminRepository ??= new SubAdminRepository(UowDbContext);
    public IUnitRepository UnitRepository => _unitRepository ??= new UnitRepository(UowDbContext);
    

}