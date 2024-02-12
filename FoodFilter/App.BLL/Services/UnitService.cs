using App.BLL.DTO;
using App.Common;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class UnitService : BaseEntityService<App.BLL.DTO.Unit, App.Domain.Unit, IUnitRepository>, IUnitService
{
    protected IAppUOW Uow;

    public UnitService(IAppUOW uow, IMapper<Unit, Domain.Unit> mapper)
        : base(uow.UnitRepository, mapper)
    {
        Uow = uow;
    }


    public decimal ConvertToGrams(decimal amount, string unitType)
    {
        var conversionFactor = GetConversionFactor(unitType);

        return amount * conversionFactor;
    }

    private decimal GetConversionFactor(string unitType)
    {
        //todo: add to unit not only unit short name but also unit name.
        switch (unitType)
        {
            case UnitTypes.Kg:
                return 1000;  // 1 kg = 1000 grams
            case UnitTypes.G:
                return 1;
            case UnitTypes.Mg:
                return 0.001m; // 1 mg = 0.001 grams
            default:
                throw new ArgumentOutOfRangeException(nameof(unitType), unitType, "Unsupported unit type");
        }
    }
}