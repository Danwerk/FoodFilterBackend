using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace DAL.EF.Repositories;

public class ImageRepository: EFBaseRepository<Image, ApplicationDbContext>, IImageRepository
{
    public ImageRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
    
}