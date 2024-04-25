using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class RestaurantClaimService : BaseEntityService<App.BLL.DTO.RestaurantClaim, App.Domain.RestaurantClaim,
        IRestaurantClaimRepository>,
    IRestaurantClaimService
{
    protected IAppUOW Uow;

    public RestaurantClaimService(IAppUOW uow, IMapper<RestaurantClaim, Domain.RestaurantClaim> mapper)
        : base(uow.RestaurantClaimRepository, mapper)
    {
        Uow = uow;
    }


    public async Task<IEnumerable<RestaurantClaim>> AllAsync(Guid restaurantId)
    {
        var restaurantClaims = await Uow.RestaurantClaimRepository.AllAsync(restaurantId);

        var restaurantClaimDtos = restaurantClaims.Select(r => Mapper.Map(r)).ToList();

        return restaurantClaimDtos!;
    }
}