using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using DAL.EF.Repositories;

namespace DAL.EF;

public class AppUOW : EFBaseUOW<ApplicationDbContext>, IAppUOW
{
    public AppUOW(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

   
    public IUnitRepository? _unitRepository;

    public IUnitRepository UnitRepository => _unitRepository ??= new UnitRepository(UowDbContext);
    

}