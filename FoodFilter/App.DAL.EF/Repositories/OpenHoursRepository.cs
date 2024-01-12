using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace DAL.EF.Repositories;

public class OpenHoursRepository : EFBaseRepository<OpenHours, ApplicationDbContext>, IOpenHoursRepository
{
    public OpenHoursRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}