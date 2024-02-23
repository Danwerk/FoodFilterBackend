using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IRestaurantRepository : IBaseRepository<Restaurant>
{
    Task<List<Restaurant>?> SearchRestaurantsAsync(string? restaurantName, string? city, string? street, string? streetNumber);

    Task<Restaurant?> FindByUserIdAsync(Guid userId);

    Restaurant Edit(Restaurant entity);
    IEnumerable<Restaurant> GetAll(int limit, string? search);

    Task<List<Restaurant>?> GetUnapprovedRestaurants();
    
    Task<List<Restaurant>?> GetApprovedRestaurants();
    Task<List<Restaurant>?> GetPendingRestaurants();
    Task<List<Restaurant>?> GetExpiredRestaurants();

}