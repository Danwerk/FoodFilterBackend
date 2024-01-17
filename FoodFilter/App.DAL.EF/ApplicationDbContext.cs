using App.Domain;
using App.Domain.Identity;
using Domain.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole, 
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public DbSet<Allergen> Allergens { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Food> Foods { get; set; } = default!;
    public DbSet<FoodAllergen> FoodAllergens { get; set; } = default!;
    public DbSet<FoodIngredient> FoodIngredients { get; set; } = default!;
    public DbSet<FoodNutrient> FoodNutrients { get; set; } = default!;
    public DbSet<Ingredient> Ingredients { get; set; } = default!;
    public DbSet<Nutrient> Nutrients { get; set; } = default!;
    public DbSet<OpenHours> OpenHours { get; set; } = default!;
    public DbSet<Restaurant> Restaurants { get; set; } = default!;
    public DbSet<SubAdmin> SubAdmins { get; set; } = default!;
    public DbSet<Unit> Units { get; set; } = default!;
    public DbSet<AppRefreshToken> AppRefreshTokens { get; set; } = default!;
    
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        builder.Entity<AppUser>()
            .HasMany(e => e.AppUserRoles)
            .WithOne(e => e.AppUser)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        
        builder.Entity<AppRole>()
            .HasMany(e => e.AppUserRoles)
            .WithOne(e => e.AppRole)
            .HasForeignKey(e => e.RoleId)
            .IsRequired();
        
        // disable cascade delete
        foreach (var foreignKey in builder.Model
                     .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
       
    }
}