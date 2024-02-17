using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class NutrientService : BaseEntityService<App.BLL.DTO.Nutrient, App.Domain.Nutrient, INutrientRepository>, INutrientService
{
    protected IAppUOW Uow;

    public NutrientService(IAppUOW uow, IMapper<Nutrient, Domain.Nutrient> mapper)
        : base(uow.NutrientRepository, mapper)
    {
        Uow = uow;
    }
}