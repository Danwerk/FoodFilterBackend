using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class RestaurantMapper: BaseMapper<BLL.DTO.Restaurant, App.Domain.Restaurant>
{
    public RestaurantMapper(IMapper mapper) : base(mapper)
    {
    }
}