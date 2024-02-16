using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class AllergenService :
    BaseEntityService<App.BLL.DTO.Allergen, App.Domain.Allergen, IAllergenRepository>, IAllergenService
{
    protected IAppUOW Uow;

    public AllergenService(IAppUOW uow, IMapper<Allergen, Domain.Allergen> mapper) 
        : base(uow.AllergenRepository, mapper)
    {
        Uow = uow; 
    }
}