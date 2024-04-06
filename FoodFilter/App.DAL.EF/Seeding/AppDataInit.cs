using System.Security.Claims;
using App.Common;
using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Seeding;

public static class AppDataInit
{
    private static Guid adminId = Guid.Parse("465385c9-dc97-4053-93af-4b78ff40fa3e");
    private static Guid userId = Guid.Parse("5581fb33-d4b2-4ef2-8bed-3a9c585f045f");


    public static void MigrateDatabase(ApplicationDbContext context)
    {
        context.Database.Migrate();
    }

    public static void DropDatabase(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
    }


    public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        if (userManager == null || roleManager == null)
        {
            throw new NullReferenceException("userManager or roleManager cannot be null!");
        }

        var roles = new (string name, string displayName)[]
        {
            (RoleNames.Admin, "System administrator"),
            (RoleNames.Restaurant, "System restaurant"),
            ("user", "Normal system user")
        };

        foreach (var roleInfo in roles)
        {
            var role = roleManager.FindByNameAsync(roleInfo.name).Result;
            if (role == null)
            {
                var identityResult = roleManager.CreateAsync(new AppRole()
                {
                    Name = roleInfo.name,
                    //DisplayName = roleInfo.displayName
                }).Result;
                if (!identityResult.Succeeded)
                {
                    throw new ApplicationException("Role creation failed");
                }
            }
        }

        var users = new (string username,
            string password,
            bool isApproved,
            string roles)[]
            {
                ("admin@app.com", "Foo.bar.1", true, "user,admin"),
                // ("restaurant@app.com", "Foo.bar.2", false, "restaurant"),
                ("newuser2@itcollege.ee", "Coca.Cola1", true, ""),
            };

        foreach (var userInfo in users)
        {
            var user = userManager.FindByEmailAsync(userInfo.username).Result;
            if (user == null)
            {
                user = new AppUser()
                {
                    Email = userInfo.username,
                    // FirstName = userInfo.firstName,
                    // LastName = userInfo.lastName,
                    IsApproved = userInfo.isApproved,
                    UserName = userInfo.username,
                    EmailConfirmed = true
                };
                var identityResult = userManager.CreateAsync(user, userInfo.password).Result;
                // identityResult =  userManager.AddClaimAsync(user, new Claim("aspnet.firstname",user.FirstName)).Result;
                // identityResult =  userManager.AddClaimAsync(user, new Claim("aspnet.lastname",user.LastName)).Result;

                if (!identityResult.Succeeded)
                {
                    throw new ApplicationException("Cannot create user!");
                }
            }

            if (!string.IsNullOrWhiteSpace(userInfo.roles))
            {
                var identityResultRole = userManager.AddToRolesAsync(user,
                    userInfo.roles.Split(",").Select(r => r.Trim())
                ).Result;
            }
        }
    }


    // public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    // {
    //     // Seed single admin
    //     (Guid id, string email, string password) adminData = (adminId, "admin@app.com", "Foo.bar.1");
    //     var adminUser = userManager.FindByEmailAsync(adminData.email).Result;
    //
    //     if (adminUser == null)
    //     {
    //         adminUser = new AppUser()
    //         {
    //             Id = adminData.id,
    //             Email = adminData.email,
    //             UserName = adminData.email,
    //             FirstName = "Admin",
    //             LastName = "App",
    //             EmailConfirmed = true,
    //         };
    //         var result = userManager.CreateAsync(adminUser, adminData.password).Result;
    //         if (!result.Succeeded)
    //         {
    //             throw new ApplicationException("Cannot seed admin users");
    //         }
    //     }
    //     
    //     // Seed single user
    //     (Guid id, string email, string password) userData = (userId, "user@app.com", "Foo.bar.2");
    //     var user = userManager.FindByEmailAsync(userData.email).Result;
    //
    //     if (user == null)
    //     {
    //         user = new AppUser()
    //         {
    //             Id = userData.id,
    //             Email = userData.email,
    //             FirstName = "Appuser",
    //             LastName = "User",
    //             UserName = userData.email,
    //             EmailConfirmed = true,
    //         };
    //         var result = userManager.CreateAsync(user, userData.password).Result;
    //         if (!result.Succeeded)
    //         {
    //             throw new ApplicationException("Cannot seed users");
    //         }
    //     }
    //     
    // }
    //
    public static void SeedData(ApplicationDbContext context)
    {
        SeedDataUnits(context);


        context.SaveChanges();
    }

    public static void SeedDataUnits(ApplicationDbContext context)
    {
        if (context.Units.Any()) return;

        context.Units.Add(new Unit()
            {
                UnitName = "kg"
            }
        );
        context.Units.Add(new Unit()
            {
                UnitName = "g"
            }
        );
    }


    // public static void SeedProducts(ApplicationDbContext context)
    // {
    //     if (context.Products.Any()) return;
    //
    //     context.Products.Add(new Product()
    //         {
    //             Image = "img url",
    //             Name = "Maasikas",
    //             Description = "Kreeka",
    //         }
    //     );
    // }

    // public static void SeedDataProducts(ApplicationDbContext context)
    // {
    //     if (context.Products.Any()) return;
    //
    //     context.Products.Add(new Product()
    //     {
    //         Image =
    //             "https://thumbs.dreamstime.com/z/kg-fresh-strawberries-plastic-box-gray-wooden-background-178246910.jpg",
    //         Name = "Strawberry",
    //         Description = "Greece",
    //         Quantity = 30,
    //         Unit = unit,
    //
    //     });
    // }
}