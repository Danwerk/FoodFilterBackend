using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace DAL.EF.Repositories;

public class IngredientRepository : EFBaseRepository<Ingredient, ApplicationDbContext>, IIngredientRepository
{
    public IngredientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
    
}