using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ClaimMapper: BaseMapper<BLL.DTO.Claim, App.Domain.Claim>
{
    public ClaimMapper(IMapper mapper) : base(mapper)
    {
    }
}