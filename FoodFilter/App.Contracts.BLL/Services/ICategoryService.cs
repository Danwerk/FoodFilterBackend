using App.BLL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface ICategoryService: IBaseRepository<App.BLL.DTO.Category>
{
}