using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUnitRepository : IBaseRepository<Unit>,  IUnitRepositoryCustom<Unit>
{
    Task<Unit?> FirstOrDefaultAsync(string name);

    
}

public interface IUnitRepositoryCustom<TEntity>
{

    //add here shared methods between repo and service
   
}