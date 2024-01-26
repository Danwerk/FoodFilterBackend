using App.Domain;

namespace App.Common;

public static class RestaurantHelperMethods
{
    // Helper method to construct full address from pieces
    public static string ConstructFullAddress(string city, string street, string streetNumber)
    {
        if (string.IsNullOrWhiteSpace(city) && string.IsNullOrWhiteSpace(street) && string.IsNullOrWhiteSpace(streetNumber))
        {
            return string.Empty;
        }
        return $"{city}, {street} {streetNumber}";
    }
}