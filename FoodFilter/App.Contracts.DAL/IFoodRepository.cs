using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IFoodRepository : IBaseRepository<Food>
{
    new Task<Food?> FindAsync(Guid foodId);
    new Task<IEnumerable<Food>> AllAsync();
    IEnumerable<Food> GetAll(Guid restaurantId, int limit, string? search);
    Task<IEnumerable<Food>> AllAsync(Guid restaurantId);
    Task<IEnumerable<Food>> PublishedAllAsync(Guid restaurantId);


    // Task<Food> Edit(Food entity);
}