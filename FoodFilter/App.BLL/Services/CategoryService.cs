using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class CategoryService: BaseEntityService<App.BLL.DTO.Category, App.Domain.Category, ICategoryRepository >, ICategoryService
{
    protected IAppUOW Uow;

    public CategoryService(IAppUOW uow, IMapper<Category, Domain.Category> mapper) 
        : base(uow.CategoryRepository, mapper)
    {
        Uow = uow; 
    }

   
}  