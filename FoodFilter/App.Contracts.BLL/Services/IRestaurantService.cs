using App.BLL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IRestaurantService: IBaseRepository<App.BLL.DTO.Restaurant>
{
    Task<List<Restaurant>?> SearchRestaurantsAsync(string? searchModelRestaurantName, string? searchModelCity, string? searchModelStreet, string? searchModelStreetNumber);
    public Task Edit(Restaurant entity);
}