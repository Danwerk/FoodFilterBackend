using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IImageRepository: IBaseRepository<App.Domain.Image>
{
    public Image Add(Image entity);

}