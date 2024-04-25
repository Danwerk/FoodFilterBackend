using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class RestaurantClaimMapper: BaseMapper<BLL.DTO.RestaurantClaim, App.Domain.RestaurantClaim>
{
    public RestaurantClaimMapper(IMapper mapper) : base(mapper)
    {
    }
}