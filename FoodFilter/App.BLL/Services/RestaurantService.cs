using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class RestaurantService: BaseEntityService<App.BLL.DTO.Restaurant, App.Domain.Restaurant, IRestaurantRepository >, IRestaurantService
{
    protected IAppUOW Uow;

    public RestaurantService(IAppUOW uow, IMapper<Restaurant, Domain.Restaurant> mapper) 
        : base(uow.RestaurantRepository, mapper)
    {
        Uow = uow; 
    }
}  