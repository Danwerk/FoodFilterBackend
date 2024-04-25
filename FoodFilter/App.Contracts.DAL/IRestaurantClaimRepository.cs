using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IRestaurantClaimRepository: IBaseRepository<RestaurantClaim>
{
    Task<IEnumerable<RestaurantClaim>> AllAsync(Guid restaurantId);

}