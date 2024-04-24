using App.BLL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IRestaurantAllergenService: IBaseRepository<App.BLL.DTO.RestaurantAllergen>
{
    Task<IEnumerable<RestaurantAllergen>> AllAsync(Guid restaurantId);

    Task<RestaurantAllergen?> FindByAllergenIdAsync(Guid id);

}