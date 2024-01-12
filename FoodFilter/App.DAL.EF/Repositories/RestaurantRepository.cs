using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace DAL.EF.Repositories;

public class RestaurantRepository : EFBaseRepository<Restaurant, ApplicationDbContext>, IRestaurantRepository
{
    public RestaurantRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}