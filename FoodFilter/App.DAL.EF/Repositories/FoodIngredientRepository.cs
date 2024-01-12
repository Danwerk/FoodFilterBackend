using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace DAL.EF.Repositories;

public class FoodIngredientRepository: EFBaseRepository<FoodIngredient, ApplicationDbContext>, IFoodIngredientRepository
{
    public FoodIngredientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
    
}