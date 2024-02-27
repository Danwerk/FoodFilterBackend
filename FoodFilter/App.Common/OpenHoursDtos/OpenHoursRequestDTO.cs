namespace App.Common.OpenHoursDtos;

public class OpenHoursRequestDTO
{
    public Guid RestaurantId { get; set; }
    
    public List<OpenHoursDTO>? OpenHours { get; set; }
}