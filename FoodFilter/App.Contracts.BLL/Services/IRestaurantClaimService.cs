using App.BLL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IRestaurantClaimService: IBaseRepository<App.BLL.DTO.RestaurantClaim>
{
    Task<IEnumerable<RestaurantClaim>> AllAsync(Guid restaurantId);
    
}