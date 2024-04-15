using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class ImageService: BaseEntityService<App.BLL.DTO.Image, App.Domain.Image, IImageRepository>, IImageService
{
    protected IAppUOW Uow;

    public ImageService(IAppUOW uow, IMapper<Image, Domain.Image> mapper)
        : base(uow.ImageRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<Image?> GetImage(Guid id)
    {
        var image = await Uow.ImageRepository.FindAsync(id);
        return Mapper.Map(image);
    }
}