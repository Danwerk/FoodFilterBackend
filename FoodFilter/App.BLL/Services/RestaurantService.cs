using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Domain;
using Base.BLL;
using Base.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Image = App.BLL.DTO.Image;
using Restaurant = App.BLL.DTO.Restaurant;

namespace App.BLL.Services;

public class RestaurantService :
    BaseEntityService<App.BLL.DTO.Restaurant, App.Domain.Restaurant, IRestaurantRepository>, IRestaurantService
{
    protected IAppUOW Uow;
    private readonly IImageService _imageService;


    public RestaurantService(IAppUOW uow, IMapper<Restaurant, Domain.Restaurant> mapper, IImageService imageService)
        : base(uow.RestaurantRepository, mapper)
    {
        Uow = uow;
        _imageService = imageService;
    }

    public async Task<List<Restaurant>?> SearchRestaurantsAsync(string? restaurantName, string? city, string? street,
        string? streetNumber)
    {
        var restaurants =
            await Uow.RestaurantRepository.SearchRestaurantsAsync(restaurantName, city, street, streetNumber);
        var restaurantDtos = restaurants?.Select(r => Mapper.Map(r)).ToList();

        return restaurantDtos!;
    }


    public async Task Edit(Restaurant entity)
    {
        Uow.RestaurantRepository.Edit(Mapper.Map(entity)!);
    }

    public async Task<Restaurant?> GetRestaurant(Guid userId)
    {
        var restaurant = await Uow.RestaurantRepository.FindByUserIdAsync(userId);
        return Mapper.Map(restaurant);
    }

    public IEnumerable<Restaurant> GetAll(int limit, string? search)
    {
        var restaurants = Uow.RestaurantRepository.GetAll(limit, search);

        var restaurantDtos = restaurants.Select(r => Mapper.Map(r)).ToList();

        return restaurantDtos!;
    }

    public async Task<List<Restaurant>?> GetUnapprovedRestaurants()
    {
        var unapprovedRestaurants = await Uow.RestaurantRepository.GetUnapprovedRestaurants();

        var unapprovedRestaurantDtos = unapprovedRestaurants?.Select(r => Mapper.Map(r)).ToList();

        return unapprovedRestaurantDtos!;
    }

    public async Task<List<Restaurant>?> GetApprovedRestaurants()
    {
        var approvedRestaurants = await Uow.RestaurantRepository.GetApprovedRestaurants();

        var approvedRestaurantDtos = approvedRestaurants?.Select(r => Mapper.Map(r)).ToList();

        return approvedRestaurantDtos!;
    }

    public async Task<List<Restaurant>?> GetPendingRestaurants()
    {
        var pendingRestaurants = await Uow.RestaurantRepository.GetPendingRestaurants();

        var pendingRestaurantDtos = pendingRestaurants?.Select(r => Mapper.Map(r)).ToList();

        return pendingRestaurantDtos!;
    }

    public async Task<List<Restaurant>?> GetExpiredRestaurants()
    {
        var expiredRestaurants = await Uow.RestaurantRepository.GetExpiredRestaurants();

        var expiredRestaurantDtos = expiredRestaurants?.Select(r => Mapper.Map(r)).ToList();

        return expiredRestaurantDtos!;
    }

    public async Task<Restaurant?> ApproveRestaurantAsync(Guid id)
    {
        var restaurant = await Uow.RestaurantRepository.FindAsync(id);

        if (restaurant != null && restaurant.AppUser != null)
        {
            restaurant.AppUser.IsApproved = true;
            restaurant.AppUser.IsRejected = false;
        }


        if (restaurant != null)
        {
            var bllRestaurant = Mapper.Map(restaurant);
            Uow.RestaurantRepository.Update(restaurant);
            await Uow.SaveChangesAsync();
            return bllRestaurant;
        }

        throw new Exception("Missing restaurant for approval");
    }


    // Disapproving means that admin rejected a restaurant 
    public async Task<Restaurant?> DisapproveRestaurantAsync(Guid id)
    {
        var restaurant = await Uow.RestaurantRepository.FindAsync(id);

        if (restaurant != null && restaurant.AppUser != null)
        {
            restaurant.AppUser.IsApproved = false;
            restaurant.AppUser.IsRejected = true;
        }

        if (restaurant != null)
        {
            var bllRestaurant = Mapper.Map(restaurant);
            Uow.RestaurantRepository.Update(restaurant);
            await Uow.SaveChangesAsync();
            return bllRestaurant;
        }

        throw new Exception("Missing restaurant for disapproval");
    }

    public async Task<Restaurant?> ConfirmRestaurantPaymentAsync(Guid id)
    {
        var restaurant = await Uow.RestaurantRepository.FindAsync(id);

        if (restaurant != null)
        {
            restaurant.PaymentStartsAt = DateTime.UtcNow;
            restaurant.PaymentEndsAt = DateTime.UtcNow.AddYears(1);
        }


        if (restaurant != null)
        {
            var bllRestaurant = Mapper.Map(restaurant);
            Uow.RestaurantRepository.Update(restaurant);
            await Uow.SaveChangesAsync();
            return bllRestaurant;
        }

        throw new Exception("Missing restaurant for payment approval");
    }

    public async Task UpdateRestaurantWithImagesAsync(Restaurant restaurant, IFormFile image)
    {
        string imagePath =  await _imageService.SaveImageToFileSystemAsync(image);
        
        var existingRestaurant = await Uow.RestaurantRepository.FirstOrDefaultAsync(restaurant.Id);
        
        var restaurantImage = new App.Domain.Image()
        {
            EntityType = EntityType.Restaurant,
            Restaurant = existingRestaurant,
            IsApproved = false,
            IsMain = false,
            Url = imagePath,
            CreatedAt = DateTime.UtcNow
        };
        
        Uow.ImageRepository.Add(restaurantImage);
        await Uow.SaveChangesAsync();
    }
}