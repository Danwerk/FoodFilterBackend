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
    
    public static void SeedData(ApplicationDbContext context)
    {
        SeedDataUnits(context);
        SeedDataNutrients(context);
        SeedDataAllergens(context);

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
    
    public static void SeedDataAllergens(ApplicationDbContext context)
    {
        if (context.Allergens.Any()) return;

        context.Allergens.Add(new Allergen()
            {
                Name = "Bell pepper"
            }
        );
        context.Allergens.Add(new Allergen()
            {
                Name = "Onion"
            }
        );
        context.Allergens.Add(new Allergen()
            {
                Name = "Crustaceans"
            }
        );
        context.Allergens.Add(new Allergen()
            {
                Name = "Molluscs"
            }
        );
        context.Allergens.Add(new Allergen()
            {
                Name = "Fish"
            }
        );
        context.Allergens.Add(new Allergen()
            {
                Name = "Sesame Seeds"
            }
        );
        context.Allergens.Add(new Allergen()
            {
                Name = "Celery"
            }
        );
        context.Allergens.Add(new Allergen()
            {
                Name = "Mustard"
            }
        );
        context.Allergens.Add(new Allergen()
            {
                Name = "Sulphites"
            }
        );
        context.Allergens.Add(new Allergen()
            {
                Name = "Lupin"
            }
        );
        context.Allergens.Add(new Allergen()
            {
                Name = "Gluten"
            }
        );
        context.Allergens.Add(new Allergen()
            {
                Name = "Peanuts"
            }
        );
        context.Allergens.Add(new Allergen()
            {
                Name = "Nuts"
            }
        );
        context.Allergens.Add(new Allergen()
            {
                Name = "Soya"
            }
        );
        context.Allergens.Add(new Allergen()
            {
                Name = "Milk"
            }
        );
        context.Allergens.Add(new Allergen()
            {
                Name = "Egg"
            }
        );
        
    }

    
    public static void SeedDataNutrients(ApplicationDbContext context)
    {
        if (context.Nutrients.Any()) return;

        context.Nutrients.Add(new Nutrient()
            {
                Name = "carbohydrates"
            }
        );
        context.Nutrients.Add(new Nutrient()
            {
                Name = "fat"
            }
        );
        context.Nutrients.Add(new Nutrient()
            {
                Name = "saturatedFattyAcids"
            }
        );
        context.Nutrients.Add(new Nutrient()
            {
                Name = "protein"
            }
        );
        context.Nutrients.Add(new Nutrient()
            {
                Name = "sugar"
            }
        );
        context.Nutrients.Add(new Nutrient()
            {
                Name = "fiber"
            }
        );
        context.Nutrients.Add(new Nutrient()
            {
                Name = "salt"
            }
        );
    }
    
}