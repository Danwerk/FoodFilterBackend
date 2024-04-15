using App.BLL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IImageService: IBaseRepository<Image>
{
    Task<Image?> GetImage(Guid id);
}