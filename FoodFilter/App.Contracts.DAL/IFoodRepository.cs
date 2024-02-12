using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IFoodRepository : IBaseRepository<Food>
{
    Task<Food?> FindAsync(Guid foodId);
    Task<IEnumerable<Food>> AllAsync();

    // Task<Food> Edit(Food entity);
}