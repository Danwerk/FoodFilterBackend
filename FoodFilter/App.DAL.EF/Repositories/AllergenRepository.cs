using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories;

public class AllergenRepository : EFBaseRepository<Allergen, ApplicationDbContext>, IAllergenRepository
{
    public AllergenRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
    
    public override async Task<IEnumerable<Allergen>> AllAsync()
    {
        return await RepositoryDbSet
            .ToListAsync();
    }
    
    public override async Task<Allergen?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}