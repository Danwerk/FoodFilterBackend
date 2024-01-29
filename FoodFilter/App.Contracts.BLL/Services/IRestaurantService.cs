using App.BLL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IRestaurantService: IBaseRepository<App.BLL.DTO.Restaurant>
{
    Task<List<Restaurant>?> SearchRestaurantsAsync(string? searchModelRestaurantName, string? searchModelCity, string? searchModelStreet, string? searchModelStreetNumber);
    Task Edit(Restaurant entity);

    Task<Restaurant?> GetRestaurant(Guid userId);

    Task<List<Restaurant>?> GetUnapprovedRestaurants();

    Task<Restaurant?> ApproveRestaurantAsync(Guid id);
}