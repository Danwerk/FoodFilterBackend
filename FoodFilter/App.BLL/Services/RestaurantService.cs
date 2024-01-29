using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class RestaurantService :
    BaseEntityService<App.BLL.DTO.Restaurant, App.Domain.Restaurant, IRestaurantRepository>, IRestaurantService
{
    protected IAppUOW Uow;

    public RestaurantService(IAppUOW uow, IMapper<Restaurant, Domain.Restaurant> mapper)
        : base(uow.RestaurantRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<List<Restaurant>?> SearchRestaurantsAsync(string? restaurantName, string? city, string? street,
        string? streetNumber)
    {
        var restaurants =
            await Uow.RestaurantRepository.SearchRestaurantsAsync(restaurantName, city, street, streetNumber);
        var restaurantDtos = restaurants?.Select(r => Mapper.Map(r)).ToList();

        return restaurantDtos;
    }


    public async Task Edit(Restaurant entity)
    {
        Uow.RestaurantRepository.Edit(Mapper.Map(entity));
    }

    public async Task<Restaurant?> GetRestaurant(Guid userId)
    {
        var restaurant = await Uow.RestaurantRepository.FindByUserIdAsync(userId);
        return Mapper.Map(restaurant);
    }

    public async Task<List<Restaurant>?> GetUnapprovedRestaurants()
    {
        var unapprovedRestaurants = await Uow.RestaurantRepository.GetUnapprovedRestaurants();

        var unapprovedRestaurantDtos = unapprovedRestaurants?.Select(r => Mapper.Map(r)).ToList();

        return unapprovedRestaurantDtos;
    }

    public async Task<Restaurant?> ApproveRestaurantAsync(Guid id)
    {
        var restaurant = await Uow.RestaurantRepository.FindAsync(id);

        if (restaurant != null && restaurant.AppUser != null)
        {
            restaurant.AppUser.IsApproved = true;
        }

        var bllRestaurant = Mapper.Map(restaurant);
        
        Uow.RestaurantRepository.Update(restaurant);
        await Uow.SaveChangesAsync();
        return bllRestaurant;
    }
}