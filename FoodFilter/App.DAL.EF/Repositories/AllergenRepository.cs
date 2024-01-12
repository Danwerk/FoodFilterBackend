using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace DAL.EF.Repositories;

public class AllergenRepository : EFBaseRepository<Allergen, ApplicationDbContext>, IAllergenRepository
{
    public AllergenRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
    
    
}