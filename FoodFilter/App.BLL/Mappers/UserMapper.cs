using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class UserMapper: BaseMapper<BLL.DTO.Identity.AppUser, App.Domain.Identity.AppUser>
{
    public UserMapper(IMapper mapper) : base(mapper)
    {
    }
}