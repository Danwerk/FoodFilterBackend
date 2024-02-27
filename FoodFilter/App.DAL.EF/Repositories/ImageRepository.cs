using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories;

public class ImageRepository : EFBaseRepository<Image, ApplicationDbContext>, IImageRepository
{
    public ImageRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Image Add(Image entity)
    {
        var entry = RepositoryDbContext.Entry(entity);

        if (entry.State == EntityState.Detached)
        {
            return RepositoryDbSet.Add(entity).Entity;
        }
        else if (entry.State == EntityState.Modified)
        {
            // If the entity is in Modified state, it means it's already tracked
            // You might want to consider updating the tracked entity instead of adding
            // Or you can detach the existing entity and then add the new one
            entry.State = EntityState.Detached;
            return RepositoryDbSet.Add(entity).Entity;
        }
        else if (entry.State == EntityState.Added)
        {
            // If the entity is in Added state, it means it's already being added
            // You might want to consider not adding it again or updating the tracked entity
            return entity;
        }
        else
        {
            // Handle other states as needed
            throw new InvalidOperationException($"Unexpected entity state: {entry.State}");
        }
    }
}