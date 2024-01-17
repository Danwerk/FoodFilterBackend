using App.Domain.Identity;
using AppUser = App.BLL.DTO.Identity.AppUser;

namespace WebApp.Areas.Admin.ViewModels;

public class UsersViewModel
{
    public IEnumerable<AppUser> Users { get; set; } = default!;
}

