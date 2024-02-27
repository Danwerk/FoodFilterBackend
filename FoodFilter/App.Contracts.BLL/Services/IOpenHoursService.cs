using App.BLL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IOpenHoursService: IBaseRepository<App.BLL.DTO.OpenHours>
{
    Task SaveOpenHours(List<OpenHours> openHoursList);

}