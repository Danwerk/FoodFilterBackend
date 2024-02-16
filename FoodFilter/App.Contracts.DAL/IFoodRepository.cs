using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IFoodRepository : IBaseRepository<Food>
{
    new Task<Food?> FindAsync(Guid foodId);
    new Task<IEnumerable<Food>> AllAsync();
    Task<IEnumerable<Food>> AllAsync(Guid restaurantId);

    // Task<Food> Edit(Food entity);
}