using App.BLL.DTO;
using App.BLL.Mappers;
using App.Contracts.DAL;
using AutoMapper;
using Base.Contracts;
using DAL.EF;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App.BLL.Services.BackgroundServices;

public class PaymentExpirationBackgroundService : TimedBackgroundService
{
    private readonly IServiceProvider _services;
    private readonly RestaurantMapper _mapper;

    public PaymentExpirationBackgroundService(ILogger<PaymentExpirationBackgroundService> logger,
        IServiceProvider services, IMapper mapper)
        : base(logger, services, TimeSpan.FromHours(1))
    {
        _services = services;
        _mapper = new RestaurantMapper(mapper);
    }

    protected override async Task DoWork(object? state)
    {
        await using var scope = _services.CreateAsyncScope();
        var uow = scope.ServiceProvider.GetRequiredService<IAppUOW>();
        var restaurantService = scope.ServiceProvider.GetRequiredService<RestaurantService>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            Logger.LogInformation("Fetching data from DB...");

            // Get all restaurants whose payment is expired
            var expiredRestaurants = await restaurantService.GetExpiredRestaurants();
            Logger.LogInformation((expiredRestaurants != null).ToString());
            Logger.LogInformation("Count " + expiredRestaurants!.Count);
            if (expiredRestaurants != null)
            {
                foreach (var bllRestaurant in expiredRestaurants)
                {
                    var dalRestaurant = await uow.RestaurantRepository.FindAsync(bllRestaurant.Id);
                    if (dalRestaurant != null && dalRestaurant.AppUser != null)
                    {
                        dalRestaurant.AppUser.IsApproved = false;

                        var res = _mapper.Map(dalRestaurant);

                        uow.RestaurantRepository.Update(dalRestaurant);
                        await uow.SaveChangesAsync();
                    }
                }

            }
            else
            {
                Logger.LogWarning("Request failed or response is null.");
            }
        }
        catch (Exception ex)
        {
            Logger.LogError($"Error while fetching expired restaurants data: {ex.Message}");
        }


        await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().SaveChangesAsync();
    }
}