using System.Net.Sockets;
using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class OpenHoursService : BaseEntityService<App.BLL.DTO.OpenHours, App.Domain.OpenHours, IOpenHoursRepository>,
    IOpenHoursService
{
    protected IAppUOW Uow;

    public OpenHoursService(IAppUOW uow, IMapper<OpenHours, Domain.OpenHours> mapper)
        : base(uow.OpenHoursRepository, mapper)
    {
        Uow = uow;
    }

    public async Task SaveOpenHours(List<OpenHours> openHoursList)
    {
        try
        {
            var openHoursDal = openHoursList.Select(r => Mapper.Map(r)).ToList();
            
            await Uow.OpenHoursRepository.AddRangeAsync(openHoursDal!);

            await Uow.SaveChangesAsync();
        }
        catch(Exception e)
        {
            throw new Exception("An error occured while saving OpenHours", e);
        }
    }

    public async Task<List<OpenHours>?> GetOpeningHoursForRestaurant(Guid id)
    {
        var openHours = await Uow.OpenHoursRepository.GetOpeningHoursForRestaurant(id);
        
        var openHoursDtos = openHours!.Select(r => Mapper.Map(r)).ToList();

        return openHoursDtos!;
    }
}