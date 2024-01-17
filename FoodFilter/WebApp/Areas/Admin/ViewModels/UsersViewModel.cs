using AppUser = App.BLL.DTO.Identity.AppUser;

namespace WebApp.Areas.Admin.ViewModels;
#pragma warning disable CS1591

public class UsersViewModel
{
    public IEnumerable<AppUser> Users { get; set; } = default!;
}

