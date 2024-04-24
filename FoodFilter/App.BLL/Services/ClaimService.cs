using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class ClaimService:
    BaseEntityService<App.BLL.DTO.Claim, App.Domain.Claim, IClaimRepository>, IClaimService
{
    protected IAppUOW Uow;

    public ClaimService(IAppUOW uow, IMapper<Claim, Domain.Claim> mapper) 
        : base(uow.ClaimRepository, mapper)
    {
        Uow = uow; 
    }
}