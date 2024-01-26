using App.Domain.Identity;
using AutoMapper;

namespace App.Public.DTO;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.BLL.DTO.Identity.AppUser, App.Public.DTO.v1.User>().ReverseMap();
        CreateMap<App.BLL.DTO.Restaurant, App.Public.DTO.v1.Restaurant>().ReverseMap();
        CreateMap<App.BLL.DTO.Unit, App.Public.DTO.v1.Unit>().ReverseMap();
    }
}