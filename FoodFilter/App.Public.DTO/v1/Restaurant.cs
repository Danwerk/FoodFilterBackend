namespace App.Public.DTO.v1
{
    public class RestaurantInitial
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
    }
    public class Restaurant : RestaurantInitial
    { 
        public string Name { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        // public string OpenHours { get; set; } = default!;
        public string? Website { get; set; }
        
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string StreetNumber { get; set; } = default!;

        public string Email { get; set; } = default!;
        
        public bool IsApproved { get; set; }

        // public List<Image>? Images { get; set; }
    }

    public class RestaurantEdit : Restaurant
    {
       
    }

    public class RestaurantCreate : RestaurantInitial
    {
        
    }
}