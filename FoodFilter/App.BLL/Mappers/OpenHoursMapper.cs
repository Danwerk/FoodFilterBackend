using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class OpenHoursMapper: BaseMapper<BLL.DTO.OpenHours, App.Domain.OpenHours>
{
    public OpenHoursMapper(IMapper mapper) : base(mapper)
    {
    }
}