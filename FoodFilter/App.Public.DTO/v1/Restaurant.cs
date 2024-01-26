namespace App.Public.DTO.v1
{
    public class Restaurant
    { 
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;
        // public string OpenHours { get; set; } = default!;
        public string? Website { get; set; }

        public Guid AppUserId { get; set; }

        // public List<Image>? Images { get; set; }
    }

    public class RestaurantEdit : Restaurant
    {
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string StreetNumber { get; set; } = default!;
    }
}