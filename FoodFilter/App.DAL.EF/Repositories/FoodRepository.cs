using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace DAL.EF.Repositories;

public class FoodRepository : EFBaseRepository<Food, ApplicationDbContext>, IFoodRepository
{
    public FoodRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}