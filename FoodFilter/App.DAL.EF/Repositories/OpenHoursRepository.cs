using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories;

public class OpenHoursRepository : EFBaseRepository<OpenHours, ApplicationDbContext>, IOpenHoursRepository
{
    public OpenHoursRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
    public override async Task<IEnumerable<OpenHours>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(a => a.Restaurant)
            .ToListAsync();
    }
    
    public override async Task<OpenHours?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(c => c.Restaurant)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}