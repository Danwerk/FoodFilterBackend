using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IOpenHoursRepository : IBaseRepository<OpenHours>
{
    Task AddRangeAsync(List<OpenHours> openHoursList);
}