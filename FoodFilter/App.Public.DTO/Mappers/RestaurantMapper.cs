using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;
using Restaurant = App.BLL.DTO.Restaurant;

namespace App.Public.DTO.Mappers;

public class RestaurantMapper: BaseMapper<App.BLL.DTO.Restaurant, App.Public.DTO.v1.Restaurant>
{
    public RestaurantMapper(IMapper mapper) : base(mapper)
    {
    }
    
   
    public Restaurant MapRestaurantCreate(RestaurantCreate restaurantCreateDto)
    {
        return new Restaurant()
        {
            Id = restaurantCreateDto.Id,
            AppUserId = restaurantCreateDto.AppUserId,
        };
    }

}