using App.Domain;

namespace App.Common;

public static class RestaurantHelperMethods
{
    // Helper method to construct full address from pieces
    public static string ConstructFullAddress(Restaurant source)
    {
        if (string.IsNullOrWhiteSpace(source.City) && string.IsNullOrWhiteSpace(source.Street) && string.IsNullOrWhiteSpace(source.StreetNumber))
        {
            return string.Empty;
        }
        return $"{source.City}, {source.Street} {source.StreetNumber}";
    }
}