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

    public async Task<OpenHours> SaveOpenHours(OpenHours openHours)
    {
        try
        {
            var openHoursDal = Mapper.Map(openHours);
            
            var oh = await Uow.OpenHoursRepository.AddAsync(openHoursDal!)!;

            var res = Mapper.Map(oh);
            
            return res!;
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