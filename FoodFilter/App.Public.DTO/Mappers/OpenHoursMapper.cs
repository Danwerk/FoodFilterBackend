using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class OpenHoursMapper: BaseMapper<App.BLL.DTO.OpenHours, App.Public.DTO.v1.OpenHours>
{
    public OpenHoursMapper(IMapper mapper) : base(mapper)
    {
    }
}