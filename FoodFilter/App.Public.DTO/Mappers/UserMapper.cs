using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class UserMapper: BaseMapper<App.BLL.DTO.Identity.AppUser, App.Public.DTO.v1.User>
{
    public UserMapper(IMapper mapper) : base(mapper)
    {
    }
}