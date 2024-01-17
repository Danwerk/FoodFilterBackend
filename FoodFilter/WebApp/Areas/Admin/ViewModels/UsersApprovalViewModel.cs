using App.Domain.Identity;
#pragma warning disable CS1591

namespace WebApp.Areas.Admin.ViewModels;

public class UsersApprovalViewModel
{
    public List<AppUser> UsersForApproval { get; set; } = default!;
    public Task<string> CurrentUserRole { get; set; } = default!;
}