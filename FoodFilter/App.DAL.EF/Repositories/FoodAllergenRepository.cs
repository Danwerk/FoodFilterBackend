using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace DAL.EF.Repositories;

public class FoodAllergenRepository: EFBaseRepository<FoodAllergen, ApplicationDbContext>, IFoodAllergenRepository
{
    public FoodAllergenRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
    
}