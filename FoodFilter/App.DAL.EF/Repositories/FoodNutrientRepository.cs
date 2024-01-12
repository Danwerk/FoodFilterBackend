using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace DAL.EF.Repositories;

public class FoodNutrientRepository: EFBaseRepository<FoodNutrient, ApplicationDbContext>, IFoodNutrientRepository
{
    public FoodNutrientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
    
}