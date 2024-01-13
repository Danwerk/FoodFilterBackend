using App.BLL.DTO;
using App.Domain.Identity;

namespace WebApp.Areas.Admin.ViewModels;

public class UsersApprovalViewModel
{
    public List<AppUser> UsersForApproval { get; set; } = default!;
}