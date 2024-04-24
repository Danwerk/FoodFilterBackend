using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace DAL.EF.Repositories;

public class ClaimRepository : EFBaseRepository<Claim, ApplicationDbContext>, IClaimRepository
{
    public ClaimRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}