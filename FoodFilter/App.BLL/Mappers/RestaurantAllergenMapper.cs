using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class RestaurantAllergenMapper: BaseMapper<BLL.DTO.RestaurantAllergen, App.Domain.RestaurantAllergen>
{
    public RestaurantAllergenMapper(IMapper mapper) : base(mapper)
    {
    }
}