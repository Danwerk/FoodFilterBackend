using System.Text.RegularExpressions;
using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories;

public class OpenHoursRepository : EFBaseRepository<OpenHours, ApplicationDbContext>, IOpenHoursRepository
{
    public OpenHoursRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<OpenHours>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(a => a.Restaurant)
            .ToListAsync();
    }

    public override async Task<OpenHours?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(c => c.Restaurant)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddRangeAsync(List<OpenHours> openHoursList)
    {
        try
        {
            foreach (var openHours in openHoursList)
            {
                var existingOpenHours = RepositoryDbContext.OpenHours
                    .FirstOrDefault(o => o.RestaurantId == openHours.RestaurantId && o.Day == openHours.Day);

                if (existingOpenHours != null)
                {
                    existingOpenHours.Open = openHours.Open;
                    existingOpenHours.Close = openHours.Close;

                    RepositoryDbContext.Entry(existingOpenHours).State = EntityState.Modified;
                }
                else
                {
                    await RepositoryDbContext.AddAsync(openHours);
                }
            }

            await RepositoryDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while adding a range of OpenHours.", e);
        }
    }

    public async Task<List<OpenHours>?> GetOpeningHoursForRestaurant(Guid restaurantId)
    {
        var latestEntriesByDay = await RepositoryDbSet
            .Where(o => o.RestaurantId == restaurantId)
            .OrderByDescending(o => o.CreatedAt)
            .GroupBy(o => o.Day)
            .ToListAsync();

        var latestEntries = latestEntriesByDay
            .SelectMany(group => group.Take(1))
            .OrderBy(entry =>
            {
                var orderedDays = new List<string>
                    { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
                return orderedDays.IndexOf(entry.Day);
            })
            .ToList();

        return latestEntries;
    }
}