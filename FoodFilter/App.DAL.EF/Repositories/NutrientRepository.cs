using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace DAL.EF.Repositories;

public class NutrientRepository : EFBaseRepository<Nutrient, ApplicationDbContext>, INutrientRepository
{
    public NutrientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}