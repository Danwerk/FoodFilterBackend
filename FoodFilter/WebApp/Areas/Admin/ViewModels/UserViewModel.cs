namespace WebApp.Areas.Admin.ViewModels;

public class UserViewModel
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!;
}