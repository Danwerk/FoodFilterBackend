using App.BLL.DTO;
#pragma warning disable CS1591

namespace WebApp.Areas.Admin.ViewModels;

public class RestaurantSearchModel 
{
    public string? City { get; set; }
    public string? RestaurantName { get; set; }
    public string? Street { get; set; }
    public int? StreetNumber { get; set; }
    
    public List<Restaurant>? SearchResult { get; set; }
}