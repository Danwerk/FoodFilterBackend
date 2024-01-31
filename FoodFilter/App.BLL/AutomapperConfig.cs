using System.Xml.Serialization;
using App.Common;
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
        CreateMap<BLL.DTO.Image, App.Domain.Image>().ReverseMap();
        CreateMap<BLL.DTO.Category, App.Domain.Category>().ReverseMap();
        CreateMap<App.Domain.Food, App.BLL.DTO.Food>().ReverseMap();
        CreateMap<App.Domain.Ingredient, App.BLL.DTO.Ingredient>().ReverseMap();
        CreateMap<App.Domain.Restaurant, App.BLL.DTO.Restaurant>()
            .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => src.AppUser!.IsApproved))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser!.Email))
            .ReverseMap()
            .ForPath(dest => dest.AppUser!.Email, opt => opt.MapFrom(src => src.Email))
            .ForPath(dest => dest.AppUser!.Id, opt => opt.MapFrom(src => src.AppUserId))
            .ForPath(dest => dest.AppUser!.IsApproved, opt => opt.MapFrom(src => src.IsApproved))
            .ForMember(dest => dest.AppUser, opt => opt.Ignore());
    }
}