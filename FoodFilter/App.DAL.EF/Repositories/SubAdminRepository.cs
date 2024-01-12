using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories;

public class SubAdminRepository : EFBaseRepository<SubAdmin, ApplicationDbContext>, ISubAdminRepository
{
    public SubAdminRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
    public override async Task<IEnumerable<SubAdmin>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(e => e.AppUser)
            .ToListAsync();
    }
    
    public virtual async Task<IEnumerable<SubAdmin>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Include(e => e.AppUser)
            .Where(c => c.AppUserId == userId)
            .ToListAsync();
    }

    public override async Task<SubAdmin?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(c => c.AppUser)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    
    public virtual async Task<SubAdmin?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Include(c => c.AppUser)
            .FirstOrDefaultAsync(m => m.Id == id && m.AppUserId == userId);
    }
}