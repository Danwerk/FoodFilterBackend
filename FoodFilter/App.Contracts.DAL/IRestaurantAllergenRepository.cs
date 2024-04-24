using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IRestaurantAllergenRepository: IBaseRepository<RestaurantAllergen>
{
    Task<IEnumerable<RestaurantAllergen>> AllAsync(Guid restaurantId);

    Task<RestaurantAllergen?> FindByAllergenIdAsync(Guid id);

}