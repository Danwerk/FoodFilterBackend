using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories;

public class NutrientRepository : EFBaseRepository<Nutrient, ApplicationDbContext>, INutrientRepository
{
    public NutrientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<Nutrient?> FirstOrDefaultAsync(string name)
    {
        return await RepositoryDbSet
            .FirstOrDefaultAsync(n => n.Name == name);
    }
}