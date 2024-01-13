using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class UserForApprovalMapper: BaseMapper<BLL.DTO.UserForApproval, App.Domain.Restaurant>
{
    public UserForApprovalMapper(IMapper mapper) : base(mapper)
    {
    }
}