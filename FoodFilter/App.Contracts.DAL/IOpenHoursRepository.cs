using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IOpenHoursRepository : IBaseRepository<OpenHours>
{
    Task<OpenHours>? AddAsync(OpenHours openHours);
    Task<List<OpenHours>?> GetOpeningHoursForRestaurant(Guid restaurantId);

}