using AutoMapper;

namespace App.BLL;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<BLL.DTO.Unit, App.Domain.Unit>().ReverseMap();
        CreateMap<BLL.DTO.Identity.AppUser, App.Domain.Identity.AppUser>().ReverseMap();
        CreateMap<BLL.DTO.Identity.AppRole, App.Domain.Identity.AppRole>().ReverseMap();
        CreateMap<BLL.DTO.Identity.AppUserRole, App.Domain.Identity.AppUserRole>().ReverseMap();
        CreateMap<BLL.DTO.UserForApproval, App.Domain.Restaurant>().ReverseMap();
        CreateMap<BLL.DTO.Restaurant, App.Domain.Restaurant>().ReverseMap();
    }
}