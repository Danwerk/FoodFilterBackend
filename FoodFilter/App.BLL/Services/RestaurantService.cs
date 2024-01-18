using App.BLL.DTO;
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
        var restaurantDTOs = restaurants?.Select(r => Mapper.Map(r)).ToList();

        return restaurantDTOs;
    }
}