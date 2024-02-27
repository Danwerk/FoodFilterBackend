using App.BLL.DTO;
using Base.Contracts.DAL;
using Microsoft.AspNetCore.Http;

namespace App.Contracts.BLL.Services;

public interface IRestaurantService: IBaseRepository<App.BLL.DTO.Restaurant>
{
    Task<List<Restaurant>?> SearchRestaurantsAsync(string? searchModelRestaurantName, string? searchModelCity, string? searchModelStreet, string? searchModelStreetNumber);
    Task Edit(Restaurant entity);

    Task<Restaurant?> GetRestaurant(Guid userId);
    IEnumerable<Restaurant> GetAll(int limit, string? search);

    Task<List<Restaurant>?> GetUnapprovedRestaurants();
    
    Task<List<Restaurant>?> GetApprovedRestaurants();
    Task<List<Restaurant>?> GetPendingRestaurants();
    Task<List<Restaurant>?> GetExpiredRestaurants();

    Task<Restaurant?> ApproveRestaurantAsync(Guid id);
    Task<Restaurant?> DisapproveRestaurantAsync(Guid id);
    Task<Restaurant?> ConfirmRestaurantPaymentAsync(Guid id);
    Task UpdateRestaurantWithImagesAsync(Restaurant restaurant, IFormFile image);
    
}