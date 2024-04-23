using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface INutrientRepository : IBaseRepository<Nutrient>
{
    Task<Nutrient?> FirstOrDefaultAsync(string name);
}