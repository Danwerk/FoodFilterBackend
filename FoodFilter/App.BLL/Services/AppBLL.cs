using App.BLL.Mappers;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;

namespace App.BLL.Services;

public class AppBLL : BaseBLL<IAppUOW>, IAppBLL
{
    protected IAppUOW Uow;
    private readonly AutoMapper.IMapper _mapper;

    public AppBLL(IAppUOW uow, IMapper mapper) : base(uow)
    {
        Uow = uow;
        _mapper = mapper;
    }
    
    private IUnitService? _units;
    private IRestaurantService? _restaurants;
    private IFoodService? _foods;
    private IIngredientService? _ingredients;
    private IAllergenService? _allergens;
    private INutrientService? _nutrients;
    private IIngredientNutrientService? _ingredientNutrients;
    private IFileService? _files;
    private IImageService? _images;
    private IOpenHoursService? _openHours;
    private IRestaurantAllergenService? _restaurantAllergens;
    private IClaimService? _claimService;
    
    // private IUserService? _users;
    // public IUserService UserService => _users ??= new UserService(Uow, new UserMapper(_mapper));
    
    
    public IUnitService UnitService => _units ??= new UnitService(Uow, new UnitMapper(_mapper));
    public IRestaurantService RestaurantService => _restaurants ??= new RestaurantService(Uow, new RestaurantMapper(_mapper), new FileService());
    public IFoodService FoodService => _foods ??= new FoodService(Uow, new FoodMapper(_mapper), new FileService(), new UnitService(Uow, new UnitMapper(_mapper)));
    public IIngredientService IngredientService => _ingredients ??= new IngredientService(Uow, new IngredientMapper(_mapper));
    public IAllergenService AllergenService => _allergens ??= new AllergenService(Uow, new AllergenMapper(_mapper));
    public INutrientService NutrientService => _nutrients ??= new NutrientService(Uow, new NutrientMapper(_mapper));
    public IIngredientNutrientService IngredientNutrientService => _ingredientNutrients ??= new IngredientNutrientService(Uow, new IngredientNutrientMapper(_mapper));
    public IFileService FileService => _files ??= new FileService();
    public IImageService ImageService => _images ??= new ImageService(Uow, new ImageMapper(_mapper));
    public IOpenHoursService OpenHoursService => _openHours ??= new OpenHoursService(Uow, new OpenHoursMapper(_mapper));
    public IRestaurantAllergenService RestaurantAllergenService => _restaurantAllergens ??= new RestaurantAllergenService(Uow, new RestaurantAllergenMapper(_mapper));
    public IClaimService ClaimService => _claimService ??= new ClaimService(Uow, new ClaimMapper(_mapper));
    
}