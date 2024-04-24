using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class ClaimMapper: BaseMapper<App.BLL.DTO.Claim, App.Public.DTO.v1.Claim>
{
    public ClaimMapper(IMapper mapper) : base(mapper)
    {
    }
    
}