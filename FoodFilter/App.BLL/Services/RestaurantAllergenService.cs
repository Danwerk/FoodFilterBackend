using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class RestaurantAllergenService: BaseEntityService<App.BLL.DTO.RestaurantAllergen, App.Domain.RestaurantAllergen, IRestaurantAllergenRepository>,
    IRestaurantAllergenService
{
    protected IAppUOW Uow;

    public RestaurantAllergenService(IAppUOW uow, IMapper<RestaurantAllergen, Domain.RestaurantAllergen> mapper)
        : base(uow.RestaurantAllergenRepository, mapper)
    {
        Uow = uow;
    }


    public async Task<IEnumerable<RestaurantAllergen>> AllAsync(Guid restaurantId)
    {
        var restaurantAllergens = await Uow.RestaurantAllergenRepository.AllAsync(restaurantId);
        
        var restaurantAllergenDtos = restaurantAllergens.Select(r => Mapper.Map(r)).ToList();

        return restaurantAllergenDtos!;
    }

    public async Task<RestaurantAllergen?> FindByAllergenIdAsync(Guid id)
    {
        var restaurantAllergen = await Uow.RestaurantAllergenRepository.FindByAllergenIdAsync(id);
        return Mapper.Map(restaurantAllergen);
    }
}