﻿using AutoMapper;

namespace App.BLL;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<BLL.DTO.Unit, App.Domain.Unit>().ReverseMap();
        CreateMap<BLL.DTO.UserForApproval, App.Domain.Restaurant>().ReverseMap();
    }
}