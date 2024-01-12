using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace DAL.EF.Repositories;

public class SubAdminRepository : EFBaseRepository<SubAdmin, ApplicationDbContext>, ISubAdminRepository
{
    public SubAdminRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}