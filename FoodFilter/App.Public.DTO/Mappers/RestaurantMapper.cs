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
    
    public Restaurant MapRestaurantEdit(RestaurantEdit restaurantEditDto)
    {
        // todo: images and openhours
        return new Restaurant()
        {
            Id = restaurantEditDto.Id,
            Name = restaurantEditDto.Name!,
            AppUserId = restaurantEditDto.AppUserId,
            City = restaurantEditDto.City,
            Street = restaurantEditDto.Street,
            StreetNumber = restaurantEditDto.StreetNumber,
            PhoneNumber = restaurantEditDto.PhoneNumber!,
            Website = restaurantEditDto.Website,
            PaymentStartsAt = restaurantEditDto.PaymentStartsAt,
            PaymentEndsAt = restaurantEditDto.PaymentEndsAt
        };
    }
    
    public Restaurant MapRestaurantCreate(RestaurantCreate restaurantCreateDto)
    {
        // todo: images and openhours
        return new Restaurant()
        {
            Id = restaurantCreateDto.Id,
            AppUserId = restaurantCreateDto.AppUserId,
        };
    }

}