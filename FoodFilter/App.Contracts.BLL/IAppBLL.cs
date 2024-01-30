using App.Contracts.BLL.Services;
using App.Contracts.BLL.Services.Identity;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBLL : IBaseBLL
{
    IUnitService UnitService { get; }
    IRestaurantService RestaurantService { get; }
    IFoodService FoodService { get; }
    // IUserService UserService { get; }
}